// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.CsFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator(LanguageNames.CSharp)]
public class AutoDocumentingCsGenerator : IIncrementalGenerator {

    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<AutoDocumentedData> autoDocumentPipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: TypeNames.AutoDocumentAttribute,
                predicate: static (_, _) => true,
                transform: static (context, _) => {
                    IEnumerable<AttributeData> attributes = context.Attributes.Where(attribute => attribute.AttributeClass?.MetadataName == TypeNames.AutoDocumentAttribute);
                    foreach (AttributeData? attribute in attributes) {
                        if (attribute.ConstructorArguments.Length <= 0) continue;
                        if (attribute.ConstructorArguments[0].Value is not string id) continue;
                        return new AutoDocumentedData(id, context.TargetNode.FullSpan.ToString());
                    }

                    return null;
                }
            )
            .Where(static data => data is not null)
            .Select(static (data, _) => data!);
        
        IncrementalValueProvider<string> assemblyNamePipeline = context.GetAssemblyNamePipeline();
        
        IncrementalValueProvider<ImmutableArray<(AutoDocumentedData Left, string Right)>> csData = autoDocumentPipeline
            .Combine(assemblyNamePipeline)
            .Collect();
        
        context.RegisterSourceOutput(csData, static (context, data) => {
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, "CsData"));
        });
    }
}
