﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebExtensions.Net
{
    /// <summary>
    /// Base object returned from JavaScript.
    /// </summary>
    public class BaseObject : IDisposable
    {
        internal bool IsInitialized;
        internal IWebExtensionsJSRuntime webExtensionsJSRuntime;
        internal string referenceId;
        internal string accessPath;

        /// <summary>Contains any additional data within this object instance.</summary>
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }

        internal void Initialize(IWebExtensionsJSRuntime webExtensionsJSRuntime, string referenceId, string accessPath)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                this.webExtensionsJSRuntime = webExtensionsJSRuntime;
                this.referenceId = referenceId;
                this.accessPath = accessPath;
            }
        }

        /// <summary>
        /// Initialize property if it is a base object
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        protected void InitializeProperty(string propertyName, object propertyValue)
        {
            if (propertyValue is BaseObject baseObject && !baseObject.IsInitialized)
            {
                var propertyAccessPath = string.IsNullOrEmpty(accessPath) ? propertyName : $"{accessPath}.{propertyName}";
                baseObject.Initialize(webExtensionsJSRuntime, referenceId, propertyAccessPath);
            }
        }

        /// <summary>
        /// Gets the property from the object asynchronously.
        /// </summary>
        /// <param name="propertyName">The property name to get.</param>
        /// <returns>An instance of TValue obtained by JSON-deserializing the return value.</returns>
        internal ValueTask<TValue> GetPropertyAsync<TValue>(string propertyName)
        {
            var functionIdentifier = string.IsNullOrEmpty(accessPath) ? propertyName : $"{accessPath}.{propertyName}";
            return webExtensionsJSRuntime.InvokeAsync<TValue>(InvokeObjectReferenceOption.Identifier, new InvokeObjectReferenceOption(referenceId, functionIdentifier, false));
        }

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously.
        /// </summary>
        /// <param name="function">The function to invoke.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>An instance of TValue obtained by JSON-deserializing the return value.</returns>
        internal ValueTask<TValue> InvokeAsync<TValue>(string function, params object[] args)
        {
            var functionIdentifier = string.IsNullOrEmpty(accessPath) ? function : $"{accessPath}.{function}";
            return webExtensionsJSRuntime.InvokeAsync<TValue>(InvokeObjectReferenceOption.Identifier, new InvokeObjectReferenceOption(referenceId, functionIdentifier, true), args);
        }

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously.
        /// </summary>
        /// <param name="function">The function to invoke.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>A System.Threading.Tasks.ValueTask that represents the asynchronous invocation operation.</returns>
        internal ValueTask InvokeVoidAsync(string function, params object[] args)
        {
            var functionIdentifier = string.IsNullOrEmpty(accessPath) ? function : $"{accessPath}.{function}";
            return webExtensionsJSRuntime.InvokeVoidAsync(InvokeObjectReferenceOption.Identifier, new InvokeObjectReferenceOption(referenceId, functionIdentifier, true), args);
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (!string.IsNullOrEmpty(referenceId) && webExtensionsJSRuntime != null)
            {
                webExtensionsJSRuntime.InvokeVoid(RemoveObjectReferenceOption.Identifier, new RemoveObjectReferenceOption(referenceId));
                referenceId = null;
            }
        }

        /// <summary>
        /// Finalizer to call Dispose()
        /// </summary>
        ~BaseObject()
        {
            Dispose(false);
        }
    }
}
