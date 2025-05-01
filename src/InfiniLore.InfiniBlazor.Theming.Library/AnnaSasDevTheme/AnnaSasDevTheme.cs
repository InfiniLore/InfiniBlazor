// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming.Library;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AnnaSasDevTheme {
    public static readonly ITheme DarkModeInstance = InfiniBlazorTheme.DarkModeInstance with {
        ColorAccent = "#F5A9B8"
    };
    public static readonly ITheme LightModeInstance = InfiniBlazorTheme.LightModeInstance with {
        ColorAccent = "#5BCEFA"
    };
}
