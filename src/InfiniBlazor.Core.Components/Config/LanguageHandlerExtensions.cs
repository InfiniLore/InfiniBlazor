// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LanguageHandlerExtensions {
    public static InfiniBlazorComponentsConfig AddMermaidCodeBlockHandler(this InfiniBlazorComponentsConfig config) {
        config.AddCodeBlockLanguageInjection<MermaidCodeBlockLanguageHandler>(StandardLanguageHandlers.Mermaid);
        return config;
    }
}
