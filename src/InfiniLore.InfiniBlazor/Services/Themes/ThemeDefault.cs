// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Themes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeDefault : IInfiniLoreTheme {
    public Dictionary<string, string> LightMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#FFFFFF" },
        { ThemeVariables.NavMenuItemText, "#FFFFFF" }
    };

    public Dictionary<string, string> DarkMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#000000" },
        { ThemeVariables.NavMenuItemText, "#000000" }
    };
}
