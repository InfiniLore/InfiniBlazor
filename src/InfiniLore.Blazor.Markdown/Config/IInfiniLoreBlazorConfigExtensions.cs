// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Blazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniLoreBlazorConfigExtensions {
    public static void AddMarkdown(this IInfiniLoreBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreBlazorMarkdown();
        
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.TextEditor.AddDefaultModifiers();
        markdownConfig.Parser.AddDefaultSanitizer();
        
        configure?.Invoke(markdownConfig);

        config.Services.AddSingleton<IMarkdownConfig>(FrozenMarkdownConfig.FromConfig(markdownConfig));
    }
}
