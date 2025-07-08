// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming.CssData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateThemeSymbols, GenerateVariableNames]
public partial record InfiniBlazorCssData : ICssData {
    public static InfiniBlazorCssData Instance { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Colors
    // -----------------------------------------------------------------------------------------------------------------
    #region Helpers
    public string Transparent { get; init; } = "transparent";
    #endregion
    
    #region Colors
    public string ColorRed { get; init; } = "#ef4444";
    public string ColorRedLight { get; init; } = "#f15e5e";
    public string ColorRedDark { get; init; } = "#500707";
    
    public string ColorOrange { get; init; } = "#f97316";
    public string ColorOrangeLight { get; init; } = "#fa9247";
    public string ColorOrangeDark { get; init; } = "#5e2902";

    public string ColorYellow { get; init; } = "#eab308";
    public string ColorYellowLight { get; init; } = "#f8cb45";
    public string ColorYellowDark { get; init; } = "#423202";
    
    public string ColorGreen { get; init; } = "#22c55e";
    public string ColorGreenLight { get; init; } = "#31dc70";
    public string ColorGreenDark { get; init; } = "#072a14";
    
    public string ColorCyan { get; init; } = "#06b6d4";
    public string ColorCyanLight { get; init; } = "#36dbf9";
    public string ColorCyanDark { get; init; } = "#01272e";
    
    public string ColorBlue { get; init; } = "#3b82f6";
    public string ColorBlueLight { get; init; } = "#669ef8";
    public string ColorBlueDark { get; init; } = "#031a3f";
    
    public string ColorPurple { get; init; } = "#a855f7";
    public string ColorPurpleLight { get; init; } = "#bb79f9";
    public string ColorPurpleDark { get; init; } = "#31045a";
    
    public string ColorPink { get; init; } = "#ec4899";
    public string ColorPinkLight { get; init; } = "#f17bb6";
    public string ColorPinkDark { get; init; } = "#4f082c";
    
    public string ColorGray { get; init; } = "#4b5563";
    public string ColorGrayLight { get; init; } = "#9ca3af";
    public string ColorGrayDark { get; init; } = "#1f2937";
    
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
    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string Text { get; init; }
    [CssData, InterpretAsVar(nameof(ColorRed))] public partial string TextError { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string Border { get; init; }
    
    [CssData, InterpretAsVar(nameof(Text))] public partial string SidebarHamburger{ get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string SidebarNavText{ get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string SidebarNavTextHover { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string SidebarNavButton{ get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string SidebarNavButtonHover { get; init; }
    [CssData, InterpretAsVar(nameof(Border))] public partial string SidebarNavBorder{ get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string SidebarNav { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string SidebarNavHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string UsericonAlt { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string UsericonAltHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string UsericonRing { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string UsericonStatus { get; }
    
    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string Page { get; init; }
    [CssData, InterpretAsVar(nameof(Text))] public partial string PageText { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string Section { get; init; }
    [CssData, InterpretAsVar(nameof(Border))] public partial string SectionBorder { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase60))] public partial string TableHeader { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string TableRow { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase60))] public partial string TableRowHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string EditorHeader { get; init; }
    [CssData, InterpretAsVar(nameof(Border))] public partial string EditorHeaderBorder { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string ToggleContainer { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ToggleSlider { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ToggleIconOn { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase30))] public partial string ToggleIconOff { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string ButtonDisabled { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDisabledText { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string ButtonDefault { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonDefaultText { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string ButtonDefaultHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonDefaultTextHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonPrimary { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ButtonPrimaryText { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonPrimaryHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase90))] public partial string ButtonPrimaryTextHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonTransparent { get; init; } 
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonTransparentText { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonTransparentHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonTransparentTextHover { get; init; }

    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string ButtonDark { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkText { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase95))] public partial string ButtonDarkHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkTextHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase10))] public partial string ButtonDarkRingHover { get; init; }

    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string ButtonLight { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase80))] public partial string ButtonLightText { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase00))] public partial string ButtonLightHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ButtonLightTextHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase70))] public partial string ButtonLightRingHover { get; init; }
    
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonOutline { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase05))] public partial string ButtonOutlineText { get; init; }
    [CssData, InterpretAsVar(nameof(Transparent))] public partial string ButtonOutlineHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonOutlineTextHover { get; init; }
    [CssData, InterpretAsVar(nameof(ColorBase40))] public partial string ButtonOutlineRing { get; init; }
    [CssData, InterpretAsVar(nameof(ColorAccent))] public partial string ButtonOutlineRingHover { get; init; }
   
    [CssData, InterpretAsVar(nameof(ColorBase50))] public partial string Codeblock { get; init; }
    #endregion
}
