// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.IO;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class AutoDocumentingGenerator : IIncrementalGenerator {
    
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
        IncrementalValuesProvider<RazorFileData> razorDataPipeline = context.AdditionalTextsProvider
            .Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
            .Select(static (text, ct) => {
                string? fileName = Path.GetFileName(text.Path);
                SourceText? content = text.GetText(ct);

                if (content is null || content.Length <= 0) return null;
                
                ImmutableArray<InfiniAutoDocumentComponent> matches = RazorFileExtractor.ExtractInfiniAutoDocumentComponents(content).ToImmutableArray();
                // if (matches.Length <= 0) return null;
                
                return new RazorFileData(fileName, matches);
            })
            .Where(static data => data is not null)
            .Select(static (data, _) => data!)
            ;

        IncrementalValueProvider<ImmutableArray<RazorFileData>> razorData = razorDataPipeline.Collect();
        
        context.RegisterSourceOutput(razorData, static (context, data) => {
            context.AddSource(AutoDocumenterDataWriter.FileName, AutoDocumenterDataWriter.GenerateFile(data));
        });
    }
    
}
