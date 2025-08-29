// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.IO;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class AutoDocumentingRazorGenerator : IIncrementalGenerator {
    
    // razor file
    // <InfiniAutoDocument Id="something">
    //      Body
    // </InfiniAutoDocument>
    // @code {
    // [AutoDocumentId("something"), AutoDocument]
    // public void ....
    // }
    
    // cs file -> CsLang generator
    // [AutoDocumentId("something)]
    // public void ....
    // class, property, field, method, etc...
    
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

        IncrementalValueProvider<ImmutableArray<(AutoDocumentedData Left, string Right)>> razorData = razorDataPipeline
            .Combine(assemblyNamePipeline)
            .Collect();
        
        context.RegisterSourceOutput(razorData, static (context, data) => {
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data, "RazorFileData"));
        });
    }
    
}
