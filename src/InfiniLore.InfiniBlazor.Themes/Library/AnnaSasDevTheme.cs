// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Themes.Library;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AnnaSasDevTheme : IInfiniLoreTheme {
    public FrozenDictionary<string, string> LightMode { get; } = new Dictionary<string, string>() {
        { CssVariables.NavMenuItemIcon, "#5BCEFA" },
        { CssVariables.NavMenuItemText, "#F5A9B8" }
    }.ToFrozenDictionary();

    public FrozenDictionary<string, string> DarkMode { get; } = new Dictionary<string, string>() {
        { CssVariables.NavMenuItemIcon, "#F5A9B8" },
        { CssVariables.NavMenuItemText, "#5BCEFA" }
    }.ToFrozenDictionary();
}
