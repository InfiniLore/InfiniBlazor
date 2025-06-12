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
        
        ButtonDefaultColor = DarkModeInstance.ColorBase10,
        ButtonPrimaryColor = DarkModeInstance.ColorBase80,
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Colors
    // -----------------------------------------------------------------------------------------------------------------
    #region Helpers
    [CssData] public string Transparent { get; init; } = "transparent";
    #endregion
    
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
    public string ColorAccentDark { get; init; } = "#965f25";
    public string ColorAccentLight { get; init; } = "#faa951";
    
    public string ColorBase00 { get; init; } = "#ffffff";
    public string ColorBase05 { get; init; } = "#dbdddc";
    public string ColorBase10 { get; init; } = "#b9bdbc";
    public string ColorBase20 { get; init; } = "#989d9b";
    public string ColorBase30 { get; init; } = "#767d7a";
    public string ColorBase40 { get; init; } = "#555c5a";
    public string ColorBase50 { get; init; } = "#333c39";
    public string ColorBase60 { get; init; } = "#2b3230";
    public string ColorBase70 { get; init; } = "#222826";
    public string ColorBase80 { get; init; } = "#1a1e1d";
    public string ColorBase90 { get; init; } = "#111413";
    public string ColorBase95 { get; init; } = "#090b0a";
    public string ColorBase100 { get; init; } = "#000000";
    #endregion

    #region CascadedValues
    #region Debug
    [CssData, InterpretAsVar(nameof(ColorRed))] public partial string DebugRed { get; init; }
    [CssData, InterpretAsVar(nameof(ColorOrange))] public partial string DebugOrange { get; init; }
    [CssData, InterpretAsVar(nameof(ColorYellow))] public partial string DebugYellow { get; init; }
    [CssData, InterpretAsVar(nameof(ColorGreen))] public partial string DebugGreen { get; init; }
    [CssData, InterpretAsVar(nameof(ColorCyan))] public partial string DebugCyan { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBlue))] public partial string DebugBlue { get; init; }
    [CssData, InterpretAsVar(nameof(ColorPurple))] public partial string DebugPurple { get; init; }
    [CssData, InterpretAsVar(nameof(ColorPink))] public partial string DebugPink { get; init; }
    [CssData, InterpretAsVar(nameof(ColorGray))] public partial string DebugGray { get; init; }
    [CssData, InterpretAsVar(nameof(ColorWhite))] public partial string DebugWhite { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBlack))] public partial string DebugBlack { get; init; }
    #endregion
    
    [CssData, InterpretAsVar(nameof(ColorGreen))] public partial string ColorSuccess { get; init; }
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string ColorSuccessText { get; init; } 
    [CssData] public string ColorSuccessBorder { get; init; } = "#0C4621";
    [CssData, InterpretAsVar(nameof(ColorOrange))] public partial string ColorWarning { get; init; }
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string ColorWarningText { get; init; } 
    [CssData] public string ColorWarningBorder { get; init; } = "#763202";
    [CssData, InterpretAsVar(nameof(ColorRed))] public partial string ColorError { get; init; }
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string ColorErrorText { get; init; }
    [CssData] public string ColorErrorBorder { get; init; }  = "#8D0C0C";
    
    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string TextColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorRed))] public partial string TextErrorColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string BorderColor { get; init; }
    
    [CssData, InterpretAsVar(nameof(TextColor))] public partial string SidebarHamburgerColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string SidebarNavTextColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string SidebarNavTextColorHover { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string SidebarNavButtonColor { get; init; }
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
    
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string SectionBackground { get; init; }
    [CssData, InterpretAsVar(nameof(BorderColor))] public partial string SectionBorderColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase60))] public partial string TableHeaderBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string TableRowBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase60))] public partial string TableRowBackgroundHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string EditorHeaderBackground { get; init; }
    [CssData, InterpretAsVar(nameof(BorderColor))] public partial string EditorHeaderBorder { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string ToggleContainerBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ToggleSliderBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ToggleIconColorOn { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase30))] public partial string ToggleIconColorOff { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string ButtonDisabledBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDisabledColor { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string ButtonDefaultBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonDefaultColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string ButtonDefaultBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonDefaultColorHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonPrimaryBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ButtonPrimaryColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonPrimaryBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string ButtonPrimaryColorHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonTransparentBackground { get; init; } 
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonTransparentColor { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonTransparentBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonTransparentColorHover { get; init; }

    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string ButtonDarkBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string ButtonDarkBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkColorHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkRingHover { get; init; }

    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string ButtonLightBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ButtonLightColor { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string ButtonLightBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ButtonLightColorHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ButtonLightRingHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonOutlineBackground { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonOutlineColor { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonOutlineBackgroundHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonOutlineColorHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string ButtonOutlineRing { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonOutlineRingHover { get; init; }
    
    #endregion
}
