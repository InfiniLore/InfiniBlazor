// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Themes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeDefault : IInfiniLoreTheme {
    public Dictionary<string, string> LightMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#000000" },
        { ThemeVariables.NavMenuItemText, "#000000" }
    };

    public Dictionary<string, string> DarkMode { get; } = new() {
        { ThemeVariables.NavMenuItemIcon, "#000000" },
        { ThemeVariables.NavMenuItemText, "#000000" }
    };
}
