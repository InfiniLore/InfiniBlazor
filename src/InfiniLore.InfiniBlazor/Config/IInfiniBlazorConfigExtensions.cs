// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddMarkdownLogic(this InfiniBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.AddTextEditor().AddDefaultModifiers();
        
        configure?.Invoke(markdownConfig);
    }
}
