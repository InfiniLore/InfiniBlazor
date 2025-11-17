// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace InfiniBlazor.AutoDocumentation.RazorFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class AutoDocumentingRazorGenerator : IIncrementalGenerator {
    
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<AutoDocumentedData> razorDataPipeline = context
            .GetRazorFilesPipeline()
            .Select(static (text, ct) => text.GetText(ct) is { Length: > 0 } content
                ? RazorFileExtractor.ExtractInfiniAutoDocumentComponents(content)
                : Enumerable.Empty<AutoDocumentedData>()
            )
            .SelectMany(static (data, _) => data);
        
        IncrementalValueProvider<ImmutableArray<AutoDocumentedData>> razorData = razorDataPipeline.Collect();
        
        context.RegisterSourceOutput(
            razorData.Combine(context.GetAssemblyNamePipeline().Combine(context.TryGetKrStyleBraces())),
            static (context, tuple) => {
                (ImmutableArray<AutoDocumentedData> data, (string? assemblyName, bool enableKrStyle)) = tuple;
                context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, enableKrStyle,"RazorData"));
            }
        );
        
    }
    
}
