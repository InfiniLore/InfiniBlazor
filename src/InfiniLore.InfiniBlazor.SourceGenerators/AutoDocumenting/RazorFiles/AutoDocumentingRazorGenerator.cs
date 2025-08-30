// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
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
            .Select(static (text, ct) => text.GetText(ct) is { Length: > 0 } content
                ? RazorFileExtractor.ExtractInfiniAutoDocumentComponents(content).ToImmutableArray()
                : ImmutableArray<AutoDocumentedData>.Empty
            )
            .SelectMany(static (data, _) => data);
        
        IncrementalValueProvider<ImmutableArray<AutoDocumentedData>> razorData = razorDataPipeline.Collect();
        
        context.RegisterSourceOutput(
            razorData.Combine(context.GetAssemblyNamePipeline()),
            static (context, tuple) => {
                (ImmutableArray<AutoDocumentedData> data, string? assemblyName) = tuple;
                context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, "RazorData"));
            }
        );
        
    }
    
}
