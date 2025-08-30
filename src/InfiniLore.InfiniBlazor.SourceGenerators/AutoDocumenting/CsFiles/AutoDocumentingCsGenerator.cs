// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
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
                TypeNames.AutoDocumentAttribute,
                predicate: static (_, _) => true,
                transform: static (context, _) => {
                    if (!AutoDocumentedData.TryGetFromSyntaxNode(context.TargetNode, out AutoDocumentedData? data)) return null;

                    return data;
                }
            )
            .Where(static data => data is not null)
            .Select(static (data, _) => data!);

        IncrementalValueProvider<string> assemblyNamePipeline = context.GetAssemblyNamePipeline();

        IncrementalValuesProvider<AutoDocumentedData> razorDataPipeline = context.AdditionalTextsProvider
            .Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .Select(static (text, ct) => {
                SourceText? content = text.GetText(ct);
                if (content is null || content.Length <= 0) return ImmutableArray<AutoDocumentedData>.Empty;

                ImmutableArray<AutoDocumentedData> dataArray = RazorFileExtractor.ExtractAutoDocumentMembers(content).ToImmutableArray();
                return dataArray;
            })
            .SelectMany(static (data, _) => data);

        IncrementalValueProvider<ImmutableArray<AutoDocumentedData>> combinedPipeline =
            autoDocumentPipeline
                .Combine(razorDataPipeline.Collect())
                .SelectMany((tuple, _) => {
                    (AutoDocumentedData? csharpData, ImmutableArray<AutoDocumentedData> razorArray) = tuple;
                    return new[] { csharpData }.Concat(razorArray);
                })
                .Collect();

        IncrementalValueProvider<(ImmutableArray<AutoDocumentedData> Left, string Right)> sourceOutputData = combinedPipeline.Combine(assemblyNamePipeline);

        context.RegisterSourceOutput(sourceOutputData, action: static (context, tuple) => {
            (ImmutableArray<AutoDocumentedData> data, string? assemblyName) = tuple;
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, "CsharpData"));
        });
    }
}
