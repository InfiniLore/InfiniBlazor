// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.CsFiles;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator(LanguageNames.CSharp)]
public class AutoDocumentingCsGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<AutoDocumentedData> autoDocumentPipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                TypeNames.AutoDocumentAttribute,
                predicate: static (_, _) => true,
                transform: static (context, _) => {
                    if (!AutoDocumentedData.TryGetFromSyntaxNode(context.TargetNode, out AutoDocumentedData? data)) return null;

                    return data;
                }
            )
            .Where(static data => data is not null)
            .Select(static (data, _) => data!);

        IncrementalValuesProvider<AutoDocumentedData> razorDataPipeline = context.AdditionalTextsProvider
            .Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .Select(static (text, ct) => text.GetText(ct) is { Length: > 0 } content
                ? RazorFileExtractor.ExtractAutoDocumentMembers(content).ToImmutableArray()
                : ImmutableArray<AutoDocumentedData>.Empty
            )
            .SelectMany(static (data, _) => data);

        IncrementalValueProvider<(ImmutableArray<AutoDocumentedData> CSharp, ImmutableArray<AutoDocumentedData> RazorCsharp)> data =
            autoDocumentPipeline.Collect()
            .Combine(razorDataPipeline.Collect());
        
        context.RegisterSourceOutput(
            data.Combine( context.GetAssemblyNamePipeline()),
            static (context, tuple) => {
                ((ImmutableArray<AutoDocumentedData> CSharp, ImmutableArray<AutoDocumentedData> RazorCsharp) data, string? assemblyName) = tuple;
                (ImmutableArray<AutoDocumentedData> cSharp, ImmutableArray<AutoDocumentedData> razorCsharp) = data;
                ImmutableArray<AutoDocumentedData> combinedData = cSharp.AddRange(razorCsharp);
                context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(combinedData, assemblyName, "CsharpData"));
            }
        );
    }
}
