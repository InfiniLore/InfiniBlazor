// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.CssData;
using JetBrains.Annotations;

namespace Example.InfiniBlazor.Shared.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class QueerThemeCollection : ThemeCollection {
    public const string Name = "Queer";
    public override string CollectionName => Name;
    
    protected override OrderedDictionary<ThemeMode, ICssData> Modes { get; } = new() {
        [TransMode] = EmptyCssData.Instance with {
            ColorAccent = "rgb(85, 205, 252)",
            SidebarNav = "linear-gradient(to bottom, rgb(85, 205, 252), rgb(179, 157, 233), rgb(247, 168, 184), rgb(246, 216, 221), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(246, 216, 221), rgb(247, 168, 184), rgb(179, 157, 233), rgb(85, 205, 252))",
            SidebarHamburger = $"var({InfiniBlazorCssDataVariableNames.ColorBase95})",
            SidebarNavText = $"var({InfiniBlazorCssDataVariableNames.ColorBase95})",
            SidebarNavTextHover = $"var({InfiniBlazorCssDataVariableNames.ColorBase95})",
        },
        
        [BisexualMode] = EmptyCssData.Instance with {
            ColorAccent = "rgb(255, 0, 128)",
            SidebarNav = "linear-gradient(to bottom, rgb(255, 0, 128), rgb(200, 37, 157), rgb(140, 71, 153), rgb(68, 46, 159), rgb(0, 50, 160))",
            SidebarHamburger = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
            SidebarNavText = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
            SidebarNavTextHover = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
        },
        
        [LesbianMode] = EmptyCssData.Instance with {
            ColorAccent = "rgb(213, 44, 0)",
            SidebarNav = "linear-gradient(to bottom, rgb(213, 44, 0), rgb(226, 150, 136), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(210, 127, 164), rgb(162, 2, 98))",
            SidebarHamburger = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
            SidebarNavText = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
            SidebarNavTextHover = $"var({InfiniBlazorCssDataVariableNames.ColorBase00})",
        },
    };

    private static ThemeMode TransMode { get; } = ThemeMode.AsCustom("trans");
    private static ThemeMode BisexualMode { get; } = ThemeMode.AsCustom("bisexual");
    private static ThemeMode LesbianMode { get; } = ThemeMode.AsCustom("lesbian");
}
