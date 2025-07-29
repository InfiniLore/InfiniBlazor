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
        // WTF why do I need to set this to null? https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-9.0
        // For MAUI this has to be set to null so it can overtake it as some form of an Interactive WebView?
        RenderModeProvider.InfiniRenderMode = null!;
        return config;
    }

}
