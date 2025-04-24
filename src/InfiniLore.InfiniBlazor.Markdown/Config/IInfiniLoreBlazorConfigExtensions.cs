// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.PreProcessors;
using InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniLoreBlazorConfigExtensions {
    public static void AddMarkdown(this IInfiniBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorMarkdown();
        config.Services.AddSingleton(typeof(IMarkdownParser<,>), typeof(MarkdownParser<,>));
        config.Services.AddSingleton<IMarkdownSyntaxTreeConverter<string>, ToStringConverter>();
        config.Services.AddSingleton(typeof(IMarkdownSyntaxTreeToWriterConverter<>), typeof(ToTextWriterConverter<>));

        config.Services.AddSingleton<IMarkdownPreProcessor<string>, StringInputProcessor>();
        
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.TextEditor.AddDefaultModifiers();
        config.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
        
        configure?.Invoke(markdownConfig);

        config.Services.AddSingleton<IMarkdownConfig>(FrozenMarkdownConfig.FromConfig(markdownConfig));
    }
}
