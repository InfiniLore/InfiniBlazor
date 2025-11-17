// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace InfiniBlazor.AutoDocumentation.RequiredSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class RequiredSourcesGenerator : IIncrementalGenerator{
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        context.RegisterPostInitializationOutput(static postInitializationContext => {
            postInitializationContext.AddSource(
                "AutoDocumentAttribute.g.cs",
                SourceText.From(SourceCodes.AutoDocumentAttributeSource, Encoding.UTF8)
            );
        });
    }
}
