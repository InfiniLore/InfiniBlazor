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
    [CssData] string ColorRed { get; }
    [CssData] string ColorRedLight { get; }
    [CssData] string ColorRedDark { get; }
    [CssData] string ColorOrange { get; }
    [CssData] string ColorOrangeLight { get; }
    [CssData] string ColorOrangeDark { get; }
    [CssData] string ColorYellow { get; }
    [CssData] string ColorYellowLight { get; }
    [CssData] string ColorYellowDark { get; }
    [CssData] string ColorGreen { get; }
    [CssData] string ColorGreenLight { get; }
    [CssData] string ColorGreenDark { get; }
    [CssData] string ColorCyan { get; }
    [CssData] string ColorCyanLight { get; }
    [CssData] string ColorCyanDark { get; }
    [CssData] string ColorBlue { get; }
    [CssData] string ColorBlueLight { get; }
    [CssData] string ColorBlueDark { get; }
    [CssData] string ColorPurple { get; }
    [CssData] string ColorPurpleLight { get; }
    [CssData] string ColorPurpleDark { get; }
    [CssData] string ColorPink { get; }
    [CssData] string ColorPinkLight { get; }
    [CssData] string ColorPinkDark { get; }
    [CssData] string ColorGray { get; }
    [CssData] string ColorGrayLight { get; }
    [CssData] string ColorGrayDark { get; }
    [CssData] string ColorWhite { get; }
    [CssData] string ColorBlack { get; }
    
    [CssData] string ColorAccent { get; }
    [CssData] string ColorAccentDark { get; }
    [CssData] string ColorAccentLight { get; }
    
    [CssData] string ColorBase00 { get; }
    [CssData] string ColorBase05 { get; }
    [CssData] string ColorBase10 { get; }
    [CssData] string ColorBase20 { get; }
    [CssData] string ColorBase30 { get; }
    [CssData] string ColorBase40 { get; }
    [CssData] string ColorBase50 { get; }
    [CssData] string ColorBase60 { get; }
    [CssData] string ColorBase70 { get; }
    [CssData] string ColorBase80 { get; }
    [CssData] string ColorBase90 { get; }
    [CssData] string ColorBase95 { get; }
    [CssData] string ColorBase100 { get; }
    #endregion

    #region CascadedValues
    
    [CssData] string Text { get; }
    [CssData] string TextError { get; }
    [CssData] string Border { get; }
    
    [CssData] string SidebarHamburger { get; }
    [CssData] string SidebarNavText { get; }
    [CssData] string SidebarNavTextHover { get; }
    [CssData] string SidebarNavButton { get; }
    [CssData] string SidebarNavButtonHover { get; }
    [CssData] string SidebarNavBorder { get; }
    [CssData] string SidebarNav { get; }
    [CssData] string SidebarNavHover { get; }
    
    [CssData] string UsericonAlt { get; }
    [CssData] string UsericonAltHover { get; }
    [CssData] string UsericonRing { get; }
    [CssData] string UsericonStatus { get; }
    
    [CssData] string Page { get; }
    [CssData] string PageText { get; }
    
    [CssData] string Section { get; }
    [CssData] string SectionBorder { get; }
    
    [CssData] string TableHeaderBackground { get; }
    [CssData] string TableRowBackground { get; }
    [CssData] string TableRowBackgroundHover { get; }
    
    [CssData] string EditorHeaderBackground { get; }
    [CssData] string EditorHeaderBorder { get; }
    
    [CssData] string ToggleContainerBackground { get; }
    [CssData] string ToggleSliderBackground { get; }
    [CssData] string ToggleIconColorOn { get; }
    [CssData] string ToggleIconColorOff { get; }
    
    [CssData] public string ButtonDisabled { get; }
    [CssData] public string ButtonDisabledText { get; }
    [CssData] public string ButtonDefault { get; }
    [CssData] public string ButtonDefaultText { get; }
    [CssData] public string ButtonDefaultHover { get; }
    [CssData] public string ButtonDefaultTextHover { get; }
    [CssData] public string ButtonPrimary { get; }
    [CssData] public string ButtonPrimaryText { get; }
    [CssData] public string ButtonPrimaryHover { get; }
    [CssData] public string ButtonPrimaryTextHover { get; }
    [CssData] public string ButtonTransparent { get; }
    [CssData] public string ButtonTransparentText { get; }
    [CssData] public string ButtonTransparentHover { get; }
    [CssData] public string ButtonTransparentTextHover { get; }
    [CssData] public string ButtonDark { get; }
    [CssData] public string ButtonDarkText { get; }
    [CssData] public string ButtonDarkHover { get; }
    [CssData] public string ButtonDarkTextHover { get; }
    [CssData] public string ButtonDarkRingHover { get; }
    [CssData] public string ButtonLight { get; }
    [CssData] public string ButtonLightText { get; }
    [CssData] public string ButtonLightHover { get; }
    [CssData] public string ButtonLightTextHover { get; }
    [CssData] public string ButtonLightRingHover { get; }
    [CssData] public string ButtonOutline { get; }
    [CssData] public string ButtonOutlineText { get; }
    [CssData] public string ButtonOutlineHover { get; }
    [CssData] public string ButtonOutlineTextHover { get; }
    [CssData] public string ButtonOutlineRing { get; }
    [CssData] public string ButtonOutlineRingHover { get; }
    #endregion
    #endregion
}
