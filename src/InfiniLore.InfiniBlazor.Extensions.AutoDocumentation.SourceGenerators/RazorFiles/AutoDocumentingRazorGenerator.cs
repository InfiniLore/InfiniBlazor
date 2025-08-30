// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators.RazorFiles;

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
            razorData.Combine(context.GetAssemblyNamePipeline()),
            static (context, tuple) => {
                (ImmutableArray<AutoDocumentedData> data, string? assemblyName) = tuple;
                context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, assemblyName, "RazorData"));
            }
        );
        
    }
    
}
