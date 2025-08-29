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
                    IEnumerable<AttributeData> attributes = context.Attributes.Where(attribute => attribute.AttributeClass?.MetadataName == "AutoDocumentAttribute");
                    foreach (AttributeData? attribute in attributes) {
                        if (attribute.ConstructorArguments.Length <= 0) continue;
                        if (attribute.ConstructorArguments[0].Value is not string id) continue;

                        string body = context.TargetNode.ToString()
                            .Replace($"[AutoDocument(\"{id}\")]", "")
                            .TrimStart();
                        return new AutoDocumentedData(id, $"\n    {body}\n");
                    }
                    return null;

                }   
            )
            .Where(static data => data is not null)
            .Select(static (data, _) => data!);
        
        IncrementalValueProvider<string> assemblyNamePipeline = context.GetAssemblyNamePipeline();
        
        IncrementalValueProvider<ImmutableArray<AutoDocumentedData>> csData = autoDocumentPipeline.Collect();
        IncrementalValueProvider<(ImmutableArray<AutoDocumentedData> Left, string Right)> sourceOutputData = csData.Combine(assemblyNamePipeline);
        
        context.RegisterSourceOutput(sourceOutputData, static (context, tuple) => {
            (ImmutableArray<AutoDocumentedData> data, string? assemblyName) = tuple;
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, "CsharpData"));
        } );
    }
}
