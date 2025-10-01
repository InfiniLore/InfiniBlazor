// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICssData {
    IEnumerable<(string, string)> AsCssVariables();

    #region Helpers
    [CssData] public string Transparent { get; }
    #endregion
    
    #region Colors
    [CssData] string ColorInfiniRed { get; }
    [CssData] string ColorInfiniRedLight { get; }
    [CssData] string ColorInfiniRedDark { get; }
    [CssData] string ColorInfiniOrange { get; }
    [CssData] string ColorInfiniOrangeLight { get; }
    [CssData] string ColorInfiniOrangeDark { get; }
    [CssData] string ColorInfiniYellow { get; }
    [CssData] string ColorInfiniYellowLight { get; }
    [CssData] string ColorInfiniYellowDark { get; }
    [CssData] string ColorInfiniGreen { get; }
    [CssData] string ColorInfiniGreenLight { get; }
    [CssData] string ColorInfiniGreenDark { get; }
    [CssData] string ColorInfiniCyan { get; }
    [CssData] string ColorInfiniCyanLight { get; }
    [CssData] string ColorInfiniCyanDark { get; }
    [CssData] string ColorInfiniBlue { get; }
    [CssData] string ColorInfiniBlueLight { get; }
    [CssData] string ColorInfiniBlueDark { get; }
    [CssData] string ColorInfiniPurple { get; }
    [CssData] string ColorInfiniPurpleLight { get; }
    [CssData] string ColorInfiniPurpleDark { get; }
    [CssData] string ColorInfiniPink { get; }
    [CssData] string ColorInfiniPinkLight { get; }
    [CssData] string ColorInfiniPinkDark { get; }
    [CssData] string ColorInfiniGray { get; }
    [CssData] string ColorInfiniGrayLight { get; }
    [CssData] string ColorInfiniGrayDark { get; }
    [CssData] string ColorInfiniWhite { get; }
    [CssData] string ColorInfiniBlack { get; }
    
    [CssData] string ColorInfiniAccent { get; }
    [CssData] string ColorInfiniAccentDark { get; }
    [CssData] string ColorInfiniAccentLight { get; }
    
    [CssData] string ColorInfiniBase00 { get; }
    [CssData] string ColorInfiniBase05 { get; }
    [CssData] string ColorInfiniBase10 { get; }
    [CssData] string ColorInfiniBase20 { get; }
    [CssData] string ColorInfiniBase30 { get; }
    [CssData] string ColorInfiniBase40 { get; }
    [CssData] string ColorInfiniBase50 { get; }
    [CssData] string ColorInfiniBase60 { get; }
    [CssData] string ColorInfiniBase70 { get; }
    [CssData] string ColorInfiniBase80 { get; }
    [CssData] string ColorInfiniBase90 { get; }
    [CssData] string ColorInfiniBase95 { get; }
    [CssData] string ColorInfiniBase100 { get; }
    #endregion

    #region CascadedValues
    
    [CssData] string ColorInfiniText { get; }
    [CssData] string ColorInfiniTextError { get; }
    [CssData] string ColorInfiniBorder { get; }
    
    [CssData] string ColorInfiniNavHamburger { get; }
    [CssData] string ColorInfiniNavText { get; }
    [CssData] string ColorInfiniNavTextHover { get; }
    [CssData] string ColorInfiniNavButton { get; }
    [CssData] string ColorInfiniNavButtonHover { get; }
    [CssData] string ColorInfiniNavBorder { get; }
    [CssData] string ColorInfiniNav { get; }
    [CssData] string ColorInfiniNavHorizontal { get; }
    [CssData] string ColorInfiniNavVertical { get; }
    [CssData] string ColorInfiniNavLeft { get; }
    [CssData] string ColorInfiniNavRight { get; }
    [CssData] string ColorInfiniNavBottom { get; }
    [CssData] string ColorInfiniNavTop { get; }
    [CssData] string ColorInfiniNavHover { get; }
    
    [CssData] string ColorInfiniUsericonAlt { get; }
    [CssData] string ColorInfiniUsericonAltHover { get; }
    [CssData] string ColorInfiniUsericonRing { get; }
    [CssData] string ColorInfiniUsericonStatus { get; }
    
    [CssData] string ColorInfiniPage { get; }
    [CssData] string ColorInfiniPageText { get; }
    
    [CssData] string ColorInfiniSection { get; }
    [CssData] string ColorInfiniSectionBorder { get; }
    
    [CssData] string ColorInfiniTableHeader { get; }
    [CssData] string ColorInfiniTableRow { get; }
    [CssData] string ColorInfiniTableRowHover { get; }
    
    [CssData] string ColorInfiniEditorHeader { get; }
    [CssData] string ColorInfiniEditorHeaderBorder { get; }
    
    [CssData] string ColorInfiniToggleContainer { get; }
    [CssData] string ColorInfiniToggleSlider { get; }
    [CssData] string ColorInfiniToggleIconOn { get; }
    [CssData] string ColorInfiniToggleIconOff { get; }
    
    [CssData] public string ColorInfiniButtonDisabled { get; }
    [CssData] public string ColorInfiniButtonDisabledText { get; }
    [CssData] public string ColorInfiniButtonDefault { get; }
    [CssData] public string ColorInfiniButtonDefaultText { get; }
    [CssData] public string ColorInfiniButtonDefaultHover { get; }
    [CssData] public string ColorInfiniButtonDefaultTextHover { get; }
    [CssData] public string ColorInfiniButtonPrimary { get; }
    [CssData] public string ColorInfiniButtonPrimaryText { get; }
    [CssData] public string ColorInfiniButtonPrimaryHover { get; }
    [CssData] public string ColorInfiniButtonPrimaryTextHover { get; }
    [CssData] public string ColorInfiniButtonTransparent { get; }
    [CssData] public string ColorInfiniButtonTransparentText { get; }
    [CssData] public string ColorInfiniButtonTransparentHover { get; }
    [CssData] public string ColorInfiniButtonTransparentTextHover { get; }
    [CssData] public string ColorInfiniButtonDark { get; }
    [CssData] public string ColorInfiniButtonDarkText { get; }
    [CssData] public string ColorInfiniButtonDarkHover { get; }
    [CssData] public string ColorInfiniButtonDarkTextHover { get; }
    [CssData] public string ColorInfiniButtonDarkRingHover { get; }
    [CssData] public string ColorInfiniButtonLight { get; }
    [CssData] public string ColorInfiniButtonLightText { get; }
    [CssData] public string ColorInfiniButtonLightHover { get; }
    [CssData] public string ColorInfiniButtonLightTextHover { get; }
    [CssData] public string ColorInfiniButtonLightRingHover { get; }
    [CssData] public string ColorInfiniButtonOutline { get; }
    [CssData] public string ColorInfiniButtonOutlineText { get; }
    [CssData] public string ColorInfiniButtonOutlineHover { get; }
    [CssData] public string ColorInfiniButtonOutlineTextHover { get; }
    [CssData] public string ColorInfiniButtonOutlineRing { get; }
    [CssData] public string ColorInfiniButtonOutlineRingHover { get; }
    
    [CssData] string ColorInfiniButtonOutlineSolid { get; }
    [CssData] string ColorInfiniButtonOutlineSolidText { get; }
    [CssData] string ColorInfiniButtonOutlineSolidHover { get; }
    [CssData] string ColorInfiniButtonOutlineSolidTextHover { get; }
    [CssData] string ColorInfiniButtonOutlineSolidRing { get; }
    [CssData] string ColorInfiniButtonOutlineSolidRingHover { get; }
    
    [CssData] public string ColorInfiniCodeblock { get; }
    #endregion
}
