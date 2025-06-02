// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateThemeSymbols, GenerateVariableNames]
public partial record InfiniBlazorTheme : ITheme {
    public static readonly InfiniBlazorTheme DarkModeInstance = new();
    public static readonly InfiniBlazorTheme LightModeInstance = new() {
        ColorBase00 = DarkModeInstance.ColorBase100,
        ColorBase05 = DarkModeInstance.ColorBase95,
        ColorBase10 = DarkModeInstance.ColorBase90,
        ColorBase20 = DarkModeInstance.ColorBase80,
        ColorBase30 = DarkModeInstance.ColorBase70,
        ColorBase40 = DarkModeInstance.ColorBase60,
        ColorBase50 = DarkModeInstance.ColorBase50,
        ColorBase60 = DarkModeInstance.ColorBase40,
        ColorBase70 = DarkModeInstance.ColorBase30,
        ColorBase80 = DarkModeInstance.ColorBase20,
        ColorBase90 = DarkModeInstance.ColorBase10,
        ColorBase95 = DarkModeInstance.ColorBase05,
        ColorBase100 = DarkModeInstance.ColorBase00,
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Colors
    // -----------------------------------------------------------------------------------------------------------------
    #region Colors
    public string ColorRed { get; init; } = "#ef4444";
    public string ColorOrange { get; init; } = "#f97316";
    public string ColorYellow { get; init; } = "#eab308";
    public string ColorGreen { get; init; } = "#22c55e";
    public string ColorCyan { get; init; } = "#06b6d4";
    public string ColorBlue { get; init; } = "#3b82f6";
    public string ColorPurple { get; init; } = "#a855f7";
    public string ColorPink { get; init; } = "#ec4899";
    public string ColorGray { get; init; } = "#4b5563";
    public string ColorWhite { get; init; } = "#ffffff";
    public string ColorBlack { get; init; } = "#000000";
    public string ColorAccent { get; init; } = "#fa9f3e";

    public string ColorBase00 { get; init; } = "#ffffff";
    public string ColorBase05 { get; init; } = "#f6f7f7";
    public string ColorBase10 { get; init; } = "#e1e6e4";
    public string ColorBase20 { get; init; } = "#c3ccc9";
    public string ColorBase30 { get; init; } = "#9caca5";
    public string ColorBase40 { get; init; } = "#788982";
    public string ColorBase50 { get; init; } = "#5d6f68";
    public string ColorBase60 { get; init; } = "#495852";
    public string ColorBase70 { get; init; } = "#3d4844";
    public string ColorBase80 { get; init; } = "#333c39";
    public string ColorBase90 { get; init; } = "#2b312f";
    public string ColorBase95 { get; init; } = "#171c1b";
    public string ColorBase100 { get; init; } = "#000000";
    #endregion

    #region CascadedValues
    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string TextColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase60))] public partial string BorderColor { get; init; }
    
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string SidebarHamburgerColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string SidebarNavTextColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string SidebarNavTextColorHover { get; init; }
    [CssData] public string SidebarNavButtonColor { get; init; } = "transparent";
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string SidebarNavButtonColorHover { get; init; }
    [CssData, InterpretAsVar(nameof(BorderColor))] public partial string SidebarNavBorderColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string SidebarNavBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string SidebarNavBackgroundHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string UsericonAlt { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string UsericonAltHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string UsericonRing { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string UsericonStatusBackground { get; }
    
    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string PageBackground { get; init; }
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string PageTextColor { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string SectionBackground { get; init; }
    [CssData, InterpretAsVar(nameof(BorderColor))] public partial string SectionBorderColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string TableHeaderBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string TableRowBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string TableRowBackgroundHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string EditorHeaderBackground { get; init; }
    #endregion
}
