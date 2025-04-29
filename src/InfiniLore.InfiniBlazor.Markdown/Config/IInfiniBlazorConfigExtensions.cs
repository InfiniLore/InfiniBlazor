// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddMarkdown(this IInfiniBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorMarkdown();
        config.Services.AddSingleton(typeof(IMarkdownParser<,>), typeof(MarkdownParser<,>));
        config.Services.AddSingleton<IMarkdownSyntaxTreeConverter<string>, ToStringConverter>();
        
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.AddTextEditor().AddDefaultModifiers();
        
        config.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
        
        configure?.Invoke(markdownConfig);
        config.Services.AddSingleton<IMarkdownConfig>(markdownConfig);

        foreach (ITextEditorConfig textEditor in markdownConfig.TextEditors) {
        }
    }
}
