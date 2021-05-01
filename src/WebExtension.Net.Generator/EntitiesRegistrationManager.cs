﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebExtension.Net.Generator.EntityRegistrars;
using WebExtension.Net.Generator.Helpers;
using WebExtension.Net.Generator.Models;
using WebExtension.Net.Generator.Models.Entities;
using WebExtension.Net.Generator.Models.Schema;

namespace WebExtension.Net.Generator
{
    public class EntitiesRegistrationManager
    {
        private readonly ILogger logger;
        private readonly RegistrationOptions registrationOptions;
        private readonly NamespaceEntityRegistrar namespaceEntityRegistrar;
        private readonly TypeEntityRegistrar typeEntityRegistrar;
        private readonly ClassEntityRegistrar classEntityRegistrar;

        public EntitiesRegistrationManager(ILogger logger, RegistrationOptions registrationOptions, NamespaceEntityRegistrar namespaceEntityRegistrar, TypeEntityRegistrar typeEntityRegistrar, ClassEntityRegistrar classEntityRegistrar)
        {
            this.logger = logger;
            this.registrationOptions = registrationOptions;
            this.namespaceEntityRegistrar = namespaceEntityRegistrar;
            this.typeEntityRegistrar = typeEntityRegistrar;
            this.classEntityRegistrar = classEntityRegistrar;
        }

        public EntityRegistrationResult RegisterEntities(IEnumerable<NamespaceDefinition> namespaceDefinitions)
        {
            var apiClassEntities = RegisterNamespaceTypesAndApi(namespaceDefinitions);
            RegisterApiRoot(apiClassEntities);
            MarkApiClassEntitiesTypeUsage(apiClassEntities);
            RegisterTypeEntitiesAsClassEntities();

            var namespaceEntities = namespaceEntityRegistrar.GetAllNamespaceEntities().ToDictionary(namespaceEntity => namespaceEntity.FormattedName, namespaceEntity => namespaceEntity.NamespaceDefinitions);
            return new EntityRegistrationResult(namespaceEntities, classEntityRegistrar.GetAllClassEntities());
        }

        private IEnumerable<ClassEntity> RegisterNamespaceTypesAndApi(IEnumerable<NamespaceDefinition> namespaceDefinitions)
        {
            var apiClassEntities = new List<ClassEntity>();

            foreach (var namespaceDefinition in namespaceDefinitions)
            {
                var namespaceEntity = namespaceEntityRegistrar.RegisterNamespace(namespaceDefinition);
                if (namespaceEntity is null)
                {
                    continue;
                }

                if (registrationOptions.ExcludeNamespaces.Contains(namespaceEntity.Name))
                {
                    logger.LogWarning($"Skipped namespace '{namespaceEntity.Name}'.");
                    continue;
                }

                if (!registrationOptions.IncludeNamespaces.Contains(namespaceEntity.Name))
                {
                    logger.LogError($"Namespace '{namespaceEntity.Name}' is not recognized.");
                    continue;
                }

                typeEntityRegistrar.RegisterNamespaceTypes(namespaceDefinition.Types, namespaceEntity);

                if (ShouldRegisterNamespaceApi(namespaceDefinition))
                {
                    var namespaceApiTypeDefinition = GetNamespaceApiTypeDefinition(namespaceDefinition, namespaceEntity);
                    var classEntity = classEntityRegistrar.RegisterNamespaceApi(namespaceApiTypeDefinition, namespaceEntity);
                    apiClassEntities.Add(classEntity);
                }
            }

            return apiClassEntities;
        }

        private static bool ShouldRegisterNamespaceApi(NamespaceDefinition namespaceDefinition)
        {
            return !(namespaceDefinition.Events is null && namespaceDefinition.Functions is null && namespaceDefinition.Properties is null);
        }

        private TypeDefinition GetNamespaceApiTypeDefinition(NamespaceDefinition namespaceDefinition, NamespaceEntity namespaceEntity)
        {
            var functions = new List<FunctionDefinition>();
            var properties = new Dictionary<string, PropertyDefinition>();

            if (namespaceDefinition.Functions is not null)
            {
                functions.AddRange(namespaceDefinition.Functions);
            }

            if (namespaceDefinition.Properties is not null)
            {
                foreach (var propertyDefinitionPair in namespaceDefinition.Properties)
                {
                    var propertyName = propertyDefinitionPair.Key;
                    var propertyDefinition = propertyDefinitionPair.Value;
                    if (propertyDefinition.IsConstant)
                    {
                        properties.Add(propertyName, GetConstantPropertyDefinition(propertyDefinition));
                    }
                    else
                    {
                        // If this is not a constant property, convert it to a function
                        functions.Add(new FunctionDefinition()
                        {
                            Name = propertyName,
                            Description = "",//TODO: $"Gets the '{propertyName}' property.",
                            Type = ObjectType.PropertyGetterFunction,
                            Async = "true",
                            FunctionReturns = SerializationHelper.DeserializeTo<FunctionReturnDefinition>(propertyDefinition)
                        });
                    }
                }
            }

            return new TypeDefinition()
            {
                Id = namespaceEntity.FormattedName + registrationOptions.ApiClassNamePostfix,
                Description = namespaceDefinition.Description,
                Type = ObjectType.Object,
                ObjectFunctions = functions.Any() ? functions : null,
                ObjectProperties = properties.Any() ? properties : null,
                ObjectEvents = namespaceDefinition.Events
            };
        }

        private static PropertyDefinition GetConstantPropertyDefinition(PropertyDefinition propertyDefinition)
        {
            var clonePropertyDefinition = SerializationHelper.DeserializeTo<PropertyDefinition>(propertyDefinition);
            if (clonePropertyDefinition.ConstantValue.HasValue)
            {
                clonePropertyDefinition.Type = clonePropertyDefinition.ConstantValue.Value.ValueKind switch
                {
                    JsonValueKind.Number => clonePropertyDefinition.ConstantValue.Value.ToString()?.Contains('.') ?? false ? ObjectType.Number : ObjectType.Integer,
                    JsonValueKind.False => ObjectType.Boolean,
                    JsonValueKind.True => ObjectType.Boolean,
                    JsonValueKind.String => ObjectType.String,
                    _ => ObjectType.Object
                };
            }
            return clonePropertyDefinition;
        }

        private void RegisterApiRoot(IEnumerable<ClassEntity> apiClassEntities)
        {
            classEntityRegistrar.RegisterRootApi(apiClassEntities);
        }

        private void MarkApiClassEntitiesTypeUsage(IEnumerable<ClassEntity> apiClassEntities)
        {
            foreach (var classEntity in apiClassEntities)
            {
                if (classEntity.TypeDefinition is null)
                {
                    throw new InvalidOperationException("Class entity should have type definition.");
                }
                MarkTypeUsage(classEntity.TypeDefinition, classEntity.NamespaceEntity);
            }
        }

        private void MarkTypeUsage(IEnumerable<TypeReference>? typeReferences, NamespaceEntity namespaceEntity)
        {
            if (typeReferences is null)
            {
                return;
            }

            foreach (var typeReference in typeReferences)
            {
                MarkTypeUsage(typeReference, namespaceEntity);
            }
        }

        private void MarkTypeUsage(TypeReference? typeReference, NamespaceEntity namespaceEntity)
        {
            if (typeReference is null || typeReference.IsUnsupported)
            {
                return;
            }

            if (typeReference.Ref is not null)
            {
                var typeEntity = typeEntityRegistrar.GetTypeEntity(typeReference.Ref, namespaceEntity);
                if (typeEntity.IsReferenced)
                {
                    return;
                }
                typeEntity.IsReferenced = true;
                MarkTypeUsage(typeEntity.Definition, typeEntity.NamespaceEntity);
            }

            MarkTypeUsage(typeReference.ArrayItems, namespaceEntity);
            MarkTypeUsage(typeReference.FunctionParameters, namespaceEntity);
            MarkTypeUsage(typeReference.FunctionReturns, namespaceEntity);
            MarkTypeUsage(typeReference.ObjectFunctions, namespaceEntity);
            MarkTypeUsage(typeReference.ObjectProperties?.Select(property => property.Value), namespaceEntity);
            MarkTypeUsage(typeReference.TypeChoices, namespaceEntity);
        }

        private void RegisterTypeEntitiesAsClassEntities()
        {
            var typeEntities = typeEntityRegistrar.GetAllEntities();
            foreach (var typeEntity in typeEntities)
            {
                if (!typeEntity.IsReferenced)
                {
                    logger.LogWarning($"Skipped Type '{typeEntity.NamespaceQualifiedId}' because it is not referenced.");
                    continue;
                }
                classEntityRegistrar.RegisterTypeEntity(typeEntity);
            }
        }
    }
}
