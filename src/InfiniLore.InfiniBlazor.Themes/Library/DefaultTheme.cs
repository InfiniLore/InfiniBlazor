// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Themes.Library;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DefaultTheme : IInfiniLoreTheme {
    public FrozenDictionary<string, string> LightMode { get; } = new Dictionary<string, string>() {
        { CssVariables.NavMenuItemIcon, "#FFFFFF" },
        { CssVariables.NavMenuItemText, "#FFFFFF" }
    }.ToFrozenDictionary();

    public FrozenDictionary<string, string> DarkMode { get; } = new Dictionary<string, string>() {
        { CssVariables.NavMenuItemIcon, "#000000" },
        { CssVariables.NavMenuItemText, "#000000" }
    }.ToFrozenDictionary();
}
