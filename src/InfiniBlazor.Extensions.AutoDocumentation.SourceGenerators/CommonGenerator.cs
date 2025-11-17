// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace InfiniBlazor.AutoDocumentation;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommonGeneratorUtilities {
    public static IncrementalValueProvider<string> GetAssemblyNamePipeline(this IncrementalGeneratorInitializationContext context)
        => context.CompilationProvider.Select(static (compilation, _) => compilation.AssemblyName!);

    public static IncrementalValuesProvider<AdditionalText> GetRazorFilesPipeline(this IncrementalGeneratorInitializationContext context)
        => context.AdditionalTextsProvider.Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase));

    public static IncrementalValueProvider<bool> TryGetKrStyleBraces(this IncrementalGeneratorInitializationContext context) {
        return context.AnalyzerConfigOptionsProvider.Select(static (provider, _) => {
            bool globalOptionEnabled = provider.GlobalOptions.TryGetValue("inifiniblazor_autodocumentation_enablekrparsing", out string? globalSwitch)
                && globalSwitch.Equals("true", StringComparison.InvariantCultureIgnoreCase);

            bool buildPropertyEnabled = provider.GlobalOptions.TryGetValue("build_property.inifiniblazor_autodocumentation_enablekrparsing", out string? buildSwitch)
                && buildSwitch.Equals("true", StringComparison.InvariantCultureIgnoreCase);

            return globalOptionEnabled || buildPropertyEnabled;
        });
    }

}
