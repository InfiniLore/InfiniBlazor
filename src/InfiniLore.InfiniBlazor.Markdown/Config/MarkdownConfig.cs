// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownConfig(IInfiniBlazorConfig infiniBlazorConfig) {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TextEditorConfig AddTextEditor(object? key = null) {
        var config = new TextEditorConfig(infiniBlazorConfig, key);
        
        if (key is null) infiniBlazorConfig.Services.AddSingleton(TextEditorFactory.CreateTextEditor);
        else infiniBlazorConfig.Services.AddKeyedSingleton(key, TextEditorFactory.CreateKeyedTextEditor);
        return config;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddMarkdownParser<TInput, TOutput>(object? key = null) {
        var config = new MarkdownParserConfig<TInput, TOutput>(infiniBlazorConfig, key);

        if (key is null) infiniBlazorConfig.Services.AddSingleton(MarkdownParserFactory.CreateParser<TInput, TOutput>);
        else infiniBlazorConfig.Services.AddKeyedSingleton(key, MarkdownParserFactory.CreateKeyedParser<TInput, TOutput>);
        return config;
    }
}
