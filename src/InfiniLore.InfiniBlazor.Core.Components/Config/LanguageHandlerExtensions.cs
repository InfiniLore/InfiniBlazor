// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LanguageHandlerExtensions {
    
    public static InfiniBlazorComponentsConfig AddInfiniBlazorCodeBlockHandler(this InfiniBlazorComponentsConfig config) {
        config.AddCodeBlockLanguageInjection<InfiniBlazorCodeBlockLanguageHandler>(StandardLanguageHandlers.InfiniBlazor);
        return config;
    }
    
    public static InfiniBlazorComponentsConfig AddMermaidCodeBlockHandler(this InfiniBlazorComponentsConfig config) {
        config.AddCodeBlockLanguageInjection<MermaidCodeBlockLanguageHandler>(StandardLanguageHandlers.Mermaid);
        return config;
    }
}
