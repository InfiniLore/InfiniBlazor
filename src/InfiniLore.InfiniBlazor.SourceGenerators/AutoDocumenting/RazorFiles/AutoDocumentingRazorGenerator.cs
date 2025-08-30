// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class AutoDocumentingRazorGenerator : IIncrementalGenerator {
    
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<AutoDocumentedData> razorDataPipeline = context.AdditionalTextsProvider
            .Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .Select(static (text, ct) => {
                SourceText? content = text.GetText(ct);

                if (content is null || content.Length <= 0) return ImmutableArray<AutoDocumentedData>.Empty;
                
                ImmutableArray<AutoDocumentedData> dataArray = RazorFileExtractor.ExtractInfiniAutoDocumentComponents(content).ToImmutableArray();
                return dataArray;
            })
            .SelectMany(static (data, _) => data);

        IncrementalValueProvider<string> assemblyNamePipeline = context.GetAssemblyNamePipeline();
        
        IncrementalValueProvider<ImmutableArray<AutoDocumentedData>> razorData = razorDataPipeline.Collect();
        IncrementalValueProvider<(ImmutableArray<AutoDocumentedData> Left, string Right)> sourceOutputData = razorData.Combine(assemblyNamePipeline);
        
        context.RegisterSourceOutput(sourceOutputData, static (context, tuple) => {
            (ImmutableArray<AutoDocumentedData> data, string? assemblyName) = tuple;
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, "RazorData"));
        });
        
    }
    
}
