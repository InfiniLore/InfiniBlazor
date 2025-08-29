// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommonGeneratorUtilities {
    public static IncrementalValueProvider<string> GetAssemblyNamePipeline(this IncrementalGeneratorInitializationContext context) {
        IncrementalValueProvider<string> assemblyNamePipeline = context.CompilationProvider
            .Select(static (compilation, _) => compilation.AssemblyName!);
        return assemblyNamePipeline;
    }
}
