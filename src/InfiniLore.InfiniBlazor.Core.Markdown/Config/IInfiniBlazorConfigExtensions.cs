// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Markdown;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class IInfiniBlazorConfigExtensions {
    public static TConfig AddMarkdown<TConfig>(this TConfig config, Action<MarkdownConfig>? configure = null) where TConfig : class, IInfiniBlazorConfig {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorCoreMarkdown();
        
        var markdownConfig = new MarkdownConfig(config);
        
        configure?.Invoke(markdownConfig);
        
        return config;
    }
}
