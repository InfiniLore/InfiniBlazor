// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation.RazorFiles;
using InfiniBlazor.AutoDocumentation.RequiredSources;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace InfiniBlazor.AutoDocumentation.CsFiles;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator(LanguageNames.CSharp)]
public class AutoDocumentingCsGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<AutoDocumentedData> autoDocumentPipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                SourceCodes.AutoDocumentAttributeTypeName,
                predicate: static (_, _) => true,
                transform: static (context, _) => {
                    if (!AutoDocumentedData.TryGetFromSyntaxNode(context.TargetNode, out AutoDocumentedData? data)) return null;
                    return data;
                }
            )
            .Where(static data => data is not null)
            .Select(static (data, _) => data!);

        IncrementalValuesProvider<AutoDocumentedData> razorDataPipeline = context
            .GetRazorFilesPipeline()
            .Select(static (text, ct) => text.GetText(ct) is { Length: > 0 } content
                ? RazorFileExtractor.ExtractAutoDocumentMembers(content)
                : Enumerable.Empty<AutoDocumentedData>()
            )
            .SelectMany(static (data, _) => data);

        IncrementalValueProvider<(ImmutableArray<AutoDocumentedData> CSharp, ImmutableArray<AutoDocumentedData> RazorCsharp)> data =
            autoDocumentPipeline.Collect()
            .Combine(razorDataPipeline.Collect());
        
        context.RegisterSourceOutput(
            data.Combine(context.GetAssemblyNamePipeline().Combine(context.TryGetKrStyleBraces())),
            static (context, tuple) => {
                ((ImmutableArray<AutoDocumentedData> CSharp, ImmutableArray<AutoDocumentedData> RazorCsharp) data, (string? assemblyName, bool enableKrStyle)) = tuple;
                (ImmutableArray<AutoDocumentedData> cSharp, ImmutableArray<AutoDocumentedData> razorCsharp) = data;
                context.AddSource(
                    AutoDocumenterDataWriter.FileName,
                    AutoDocumenterDataWriter.GenerateFile(EnumerateCombined(cSharp, razorCsharp), assemblyName, enableKrStyle, "CsharpData")
                );
            }
        );
    }
    
    private static IEnumerable<AutoDocumentedData> EnumerateCombined(
        ImmutableArray<AutoDocumentedData> csharpData,
        ImmutableArray<AutoDocumentedData> razorCsharpData
    ) {
        foreach (AutoDocumentedData? item in csharpData) yield return item;
        foreach (AutoDocumentedData? item in razorCsharpData) yield return item;
    }

}
