// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownConfig(IInfiniBlazorConfig infiniBlazorConfig) : IMarkdownConfig {
    public readonly List<ITextEditorConfig> TextEditors = [];
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TextEditorConfig AddTextEditor(object? key = null) {
        var config = new TextEditorConfig(infiniBlazorConfig, key);
        TextEditors.Add(config);
        infiniBlazorConfig.Services.AddKeyedSingleton(key, TextEditorFactory.CreateTextEditor);
        return config;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddMarkdownParser<TInput, TOutput>(object? key = null)
        where TInput : class 
        where TOutput : class
    {
        var config = new MarkdownParserConfig<TInput, TOutput>(infiniBlazorConfig, key);

        infiniBlazorConfig.Services.AddKeyedSingleton(key, MarkdownParserFactory.CreateParser<TInput, TOutput>);
        return config;
    }
}
