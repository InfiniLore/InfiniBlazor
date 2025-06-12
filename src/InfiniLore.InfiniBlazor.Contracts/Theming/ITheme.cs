// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ITheme {
    IEnumerable<(string, string)> AsCssVariables();

    #region CssData
    #region Helpers
    [CssData] public string Transparent { get; }
    #endregion
    
    #region Colors
    [CssData, InterpretAsRgb] string ColorRedRgb { get; }
    [CssData] string ColorRed { get; }
    [CssData, InterpretAsRgb] string ColorOrangeRgb { get; }
    [CssData] string ColorOrange { get; }
    [CssData, InterpretAsRgb] string ColorYellowRgb { get; }
    [CssData] string ColorYellow { get; }
    [CssData, InterpretAsRgb] string ColorGreenRgb { get; }
    [CssData] string ColorGreen { get; }
    [CssData, InterpretAsRgb] string ColorCyanRgb { get; }
    [CssData] string ColorCyan { get; }
    [CssData, InterpretAsRgb] string ColorBlueRgb { get; }
    [CssData] string ColorBlue { get; }
    [CssData, InterpretAsRgb] string ColorPurpleRgb { get; }
    [CssData] string ColorPurple { get; }
    [CssData, InterpretAsRgb] string ColorPinkRgb { get; }
    [CssData] string ColorPink { get; }
    [CssData, InterpretAsRgb] string ColorGrayRgb { get; }
    [CssData] string ColorGray { get; }
    [CssData, InterpretAsRgb] string ColorWhiteRgb { get; }
    [CssData] string ColorWhite { get; }
    [CssData, InterpretAsRgb] string ColorBlackRgb { get; }
    [CssData] string ColorBlack { get; }

    [CssData, InterpretAsRgb] string ColorAccentRgb { get; }
    [CssData] string ColorAccent { get; }
    [CssData] string ColorAccentDark { get; }
    [CssData] string ColorAccentLight { get; }

    [CssData, InterpretAsRgb] string ColorBase00Rgb { get; }
    [CssData] string ColorBase00 { get; }
    [CssData, InterpretAsRgb] string ColorBase05Rgb { get; }
    [CssData] string ColorBase05 { get; }
    [CssData, InterpretAsRgb] string ColorBase10Rgb { get; }
    [CssData] string ColorBase10 { get; }
    [CssData, InterpretAsRgb] string ColorBase20Rgb { get; }
    [CssData] string ColorBase20 { get; }
    [CssData, InterpretAsRgb] string ColorBase30Rgb { get; }
    [CssData] string ColorBase30 { get; }
    [CssData, InterpretAsRgb] string ColorBase40Rgb { get; }
    [CssData] string ColorBase40 { get; }
    [CssData, InterpretAsRgb] string ColorBase50Rgb { get; }
    [CssData] string ColorBase50 { get; }
    [CssData, InterpretAsRgb] string ColorBase60Rgb { get; }
    [CssData] string ColorBase60 { get; }
    [CssData, InterpretAsRgb] string ColorBase70Rgb { get; }
    [CssData] string ColorBase70 { get; }
    [CssData, InterpretAsRgb] string ColorBase80Rgb { get; }
    [CssData] string ColorBase80 { get; }
    [CssData, InterpretAsRgb] string ColorBase90Rgb { get; }
    [CssData] string ColorBase90 { get; }
    [CssData, InterpretAsRgb] string ColorBase95Rgb { get; }
    [CssData] string ColorBase95 { get; }
    [CssData, InterpretAsRgb] string ColorBase100Rgb { get; }
    [CssData] string ColorBase100 { get; }
    #endregion

    #region CascadedValues
    #region Debug
    [CssData] string DebugRed  { get; }
    [CssData] string DebugOrange  { get; }
    [CssData] string DebugYellow  { get; }
    [CssData] string DebugGreen  { get; }
    [CssData] string DebugCyan  { get; }
    [CssData] string DebugBlue  { get; }
    [CssData] string DebugPurple  { get; }
    [CssData] string DebugPink  { get; }
    [CssData] string DebugGray  { get; }
    [CssData] string DebugWhite  { get; }
    [CssData] string DebugBlack  { get; }
    #endregion
    
    [CssData] string ColorSuccess { get; }
    [CssData] string ColorSuccessText { get; }
    [CssData] string ColorSuccessBorder { get; }
    [CssData] string ColorWarning { get; }
    [CssData] string ColorWarningText { get; }
    [CssData] string ColorWarningBorder { get; }
    [CssData] string ColorError { get; }
    [CssData] string ColorErrorText { get; }
    [CssData] string ColorErrorBorder { get; }
    
    [CssData] string TextColor { get; }
    [CssData] string TextErrorColor { get; }
    [CssData] string BorderColor { get; }
    
    [CssData] string SidebarHamburgerColor { get; }
    [CssData] string SidebarNavTextColor { get; }
    [CssData] string SidebarNavTextColorHover { get; }
    [CssData] string SidebarNavButtonColor { get; }
    [CssData] string SidebarNavButtonColorHover { get; }
    [CssData] string SidebarNavBorderColor { get; }
    [CssData] string SidebarNavBackground { get; }
    [CssData] string SidebarNavBackgroundHover { get; }
    
    [CssData] string UsericonAlt { get; }
    [CssData] string UsericonAltHover { get; }
    [CssData] string UsericonRing { get; }
    [CssData] string UsericonStatusBackground { get; }
    
    [CssData] string PageBackground { get; }
    [CssData] string PageTextColor { get; }
    
    [CssData] string SectionBackground { get; }
    [CssData] string SectionBorderColor { get; }
    
    [CssData] string TableHeaderBackground { get; }
    [CssData] string TableRowBackground { get; }
    [CssData] string TableRowBackgroundHover { get; }
    
    [CssData] string EditorHeaderBackground { get; }
    [CssData] string EditorHeaderBorder { get; }
    
    [CssData] string ToggleContainerBackground { get; }
    [CssData] string ToggleSliderBackground { get; }
    [CssData] string ToggleIconColorOn { get; }
    [CssData] string ToggleIconColorOff { get; }
    
    [CssData] public string ButtonDisabledBackground { get; }
    [CssData] public string ButtonDisabledColor { get; }
    [CssData] public string ButtonDefaultBackground { get; }
    [CssData] public string ButtonDefaultColor { get; }
    [CssData] public string ButtonDefaultBackgroundHover { get; }
    [CssData] public string ButtonDefaultColorHover { get; }
    [CssData] public string ButtonPrimaryBackground { get; }
    [CssData] public string ButtonPrimaryColor { get; }
    [CssData] public string ButtonPrimaryBackgroundHover { get; }
    [CssData] public string ButtonPrimaryColorHover { get; }
    [CssData] public string ButtonTransparentBackground { get; }
    [CssData] public string ButtonTransparentColor { get; }
    [CssData] public string ButtonTransparentBackgroundHover { get; }
    [CssData] public string ButtonTransparentColorHover { get; }
    [CssData] public string ButtonDarkBackground { get; }
    [CssData] public string ButtonDarkColor { get; }
    [CssData] public string ButtonDarkBackgroundHover { get; }
    [CssData] public string ButtonDarkColorHover { get; }
    [CssData] public string ButtonDarkRingHover { get; }
    [CssData] public string ButtonLightBackground { get; }
    [CssData] public string ButtonLightColor { get; }
    [CssData] public string ButtonLightBackgroundHover { get; }
    [CssData] public string ButtonLightColorHover { get; }
    [CssData] public string ButtonLightRingHover { get; }
    [CssData] public string ButtonOutlineBackground { get; }
    [CssData] public string ButtonOutlineColor { get; }
    [CssData] public string ButtonOutlineBackgroundHover { get; }
    [CssData] public string ButtonOutlineColorHover { get; }
    [CssData] public string ButtonOutlineRing { get; }
    [CssData] public string ButtonOutlineRingHover { get; }
    #endregion
    #endregion
}
