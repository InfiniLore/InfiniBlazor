// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;

namespace Example.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AnnaSasDevTheme {
    public static readonly ITheme DarkModeInstance = EmptyTheme.Instance with {
        ColorAccent = "#F5A9B8"
    };
    
    public static readonly ITheme LightModeInstance = EmptyTheme.Instance with {
        ColorAccent = "#5BCEFA",
        SidebarNav = "linear-gradient(to bottom, rgb(85, 205, 252), rgb(179, 157, 233), rgb(247, 168, 184), rgb(246, 216, 221), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(246, 216, 221), rgb(247, 168, 184), rgb(179, 157, 233), rgb(85, 205, 252))"
    };
}
