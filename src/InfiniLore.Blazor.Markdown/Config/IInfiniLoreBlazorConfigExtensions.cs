// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Blazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniLoreBlazorConfigExtensions {
    public static void AddMarkdown(this IInfiniLoreBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreBlazorMarkdown();
        
        var markdownConfig = new MarkdownConfig(config);
        
        configure?.Invoke(markdownConfig);
    }
}
