﻿namespace WebExtension.Net.Generator.CodeGeneration.CodeConverters
{
    public class ApiRootConstructorCodeConverter : ICodeConverter
    {
        private readonly string apiRootName;

        public ApiRootConstructorCodeConverter(string apiRootName)
        {
            this.apiRootName = apiRootName;
        }

        public void WriteTo(CodeWriter codeWriter, CodeWriterOptions options)
        {
            codeWriter.Properties
                .WriteLine($"private readonly WebExtensionJSRuntime webExtensionJSRuntime;");

            codeWriter.Constructors
                .WriteWithConverter(new CommentSummaryCodeConverter($"Creates a new instance of <see cref=\"{apiRootName}\" />."))
                .WriteLine($"public {apiRootName}(WebExtensionJSRuntime webExtensionJSRuntime)")
                .WriteStartBlock()
                .WriteLine($"this.webExtensionJSRuntime = webExtensionJSRuntime;")
                .WriteEndBlock();
        }
    }
}