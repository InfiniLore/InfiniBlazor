// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownConfig(InfiniBlazorConfig infiniBlazorConfig) {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TextEditorConfig AddTextEditor(object? key = null) {
        var config = new TextEditorConfig(infiniBlazorConfig, key);
        
        if (key is null) infiniBlazorConfig.Services.AddTransient(TextEditorFactory.CreateTextEditor);
        else infiniBlazorConfig.Services.AddKeyedTransient(key, TextEditorFactory.CreateKeyedTextEditor);
        return config;
    }
}
