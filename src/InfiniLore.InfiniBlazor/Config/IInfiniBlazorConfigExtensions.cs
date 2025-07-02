// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser;
using InfiniLore.InfiniBlazor.MarkdownParser.Processors.InputProcessors;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddMarkdownLogic(this InfiniBlazorConfig config, Action<MarkdownConfig>? configure = null) {
        config.Services.AddSingleton(typeof(IMarkdownParser<,>), typeof(MarkdownParser<,>));
        
        var markdownConfig = new MarkdownConfig(config);
        markdownConfig.AddTextEditor().AddDefaultModifiers();
        
        markdownConfig.AddMarkdownParser<string, string>()
            .AddInputProcessor<StringInputProcessor>();
        
        markdownConfig.AddMarkdownParser<string, MarkupString>("styled")
            .AddInputProcessor<StringInputProcessor>();
        
        config.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
        
        configure?.Invoke(markdownConfig);
    }
}
