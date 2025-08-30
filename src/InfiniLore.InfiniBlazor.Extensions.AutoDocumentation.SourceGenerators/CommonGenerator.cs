// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System;

namespace InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommonGeneratorUtilities {
    public static IncrementalValueProvider<string> GetAssemblyNamePipeline(this IncrementalGeneratorInitializationContext context) 
        => context.CompilationProvider.Select(static (compilation, _) => compilation.AssemblyName!);

    public static IncrementalValuesProvider<AdditionalText> GetRazorFilesPipeline(this IncrementalGeneratorInitializationContext context) 
        => context.AdditionalTextsProvider.Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase));
}
