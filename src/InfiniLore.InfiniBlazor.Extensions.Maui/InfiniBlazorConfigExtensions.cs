// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorConfigExtensions {
    
    public static InfiniBlazorConfig SetRenderModeForMauiBlazorHybrid(this InfiniBlazorConfig config) {
        // For MAUI this has to be set to null so it can overtake it as WebView
        RenderModeProvider.InfiniRenderMode = null!;
        return config;
    }

}
