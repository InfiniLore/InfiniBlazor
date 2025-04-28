// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Themes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeAnna : IInfiniLoreTheme {
    public Dictionary<string, string> LightMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#5BCEFA" },
        { ThemeVariables.NavMenuItemText, "#F5A9B8" }
    };

    public Dictionary<string, string> DarkMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#F5A9B8" },
        { ThemeVariables.NavMenuItemText, "#5BCEFA" }
    };
}
