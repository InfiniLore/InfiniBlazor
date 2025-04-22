// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniLoreBlazorConfigExtensions {
    public static void AddMarkdown(this IInfiniBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorMarkdown();
        config.Services.AddSingleton<IMdNodeTreeConverter<string>, NodeTreeToStringConverter>();
        config.Services.AddSingleton(typeof(IMdNodeTreeToWriterConverter<>), typeof(NodeTreeToTextWriterConverter<>));
        
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.TextEditor.AddDefaultModifiers();
        markdownConfig.Parser.AddDefaultSanitizer();
        
        configure?.Invoke(markdownConfig);

        config.Services.AddSingleton<IMarkdownConfig>(FrozenMarkdownConfig.FromConfig(markdownConfig));
    }
}
