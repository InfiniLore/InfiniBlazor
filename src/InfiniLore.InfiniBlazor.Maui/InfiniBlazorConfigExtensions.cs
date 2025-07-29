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
        // For MAUI this has to be set to null;
        RenderModeProvider.InfiniRenderMode = null!;
        return config;
    }

}
