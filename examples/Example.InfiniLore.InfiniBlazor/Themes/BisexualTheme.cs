// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;

namespace Example.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BisexualTheme {
    public static readonly ITheme DarkModeInstance = EmptyTheme.Instance with {
        ColorAccent = "#0032a0",
        SidebarNav = "linear-gradient(to bottom, rgb(255, 0, 128), rgb(200, 37, 157), rgb(140, 71, 153), rgb(68, 46, 159), rgb(0, 50, 160))"
    };
    
    public static readonly ITheme LightModeInstance = EmptyTheme.Instance with {
        ColorAccent = "#ff0080",
        SidebarNav = "linear-gradient(to bottom, rgb(255, 0, 128), rgb(200, 37, 157), rgb(140, 71, 153), rgb(68, 46, 159), rgb(0, 50, 160))"
    };
}
