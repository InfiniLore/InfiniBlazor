// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming.CssData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateThemeSymbols]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public partial record EmptyCssData : ICssData {
    public static EmptyCssData Instance { get; } = new();

    public virtual string ColorInfiniBorder { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDark { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDarkHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDarkRingHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDarkText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDarkTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDefault { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDefaultHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDefaultText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDefaultTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDisabled { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonDisabledText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonLight { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonLightHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonLightRingHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonLightText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonLightTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutline { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineRing { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineRingHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonPrimary { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonPrimaryHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonPrimaryText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonPrimaryTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonTransparent { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonTransparentHover { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonTransparentText { get; init; } = string.Empty;
    public virtual string ColorInfiniButtonTransparentTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniAccent { get; init; } = string.Empty;
    public virtual string ColorInfiniAccentDark { get; init; } = string.Empty;
    public virtual string ColorInfiniAccentLight { get; init; } = string.Empty;
    public virtual string ColorInfiniBase00 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase05 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase10 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase100 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase20 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase30 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase40 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase50 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase60 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase70 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase80 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase90 { get; init; } = string.Empty;
    public virtual string ColorInfiniBase95 { get; init; } = string.Empty;
    public virtual string ColorInfiniBlack { get; init; } = string.Empty;
    public virtual string ColorInfiniBlue { get; init; } = string.Empty;
    public virtual string ColorInfiniBlueDark { get; init; } = string.Empty;
    public virtual string ColorInfiniBlueLight { get; init; } = string.Empty;
    public virtual string ColorInfiniCyan { get; init; } = string.Empty;
    public virtual string ColorInfiniCyanDark { get; init; } = string.Empty;
    public virtual string ColorInfiniCyanLight { get; init; } = string.Empty;
    public virtual string ColorInfiniGray { get; init; } = string.Empty;
    public virtual string ColorInfiniGrayDark { get; init; } = string.Empty;
    public virtual string ColorInfiniGrayLight { get; init; } = string.Empty;
    public virtual string ColorInfiniGreen { get; init; } = string.Empty;
    public virtual string ColorInfiniGreenDark { get; init; } = string.Empty;
    public virtual string ColorInfiniGreenLight { get; init; } = string.Empty;
    public virtual string ColorInfiniOrange { get; init; } = string.Empty;
    public virtual string ColorInfiniOrangeDark { get; init; } = string.Empty;
    public virtual string ColorInfiniOrangeLight { get; init; } = string.Empty;
    public virtual string ColorInfiniPink { get; init; } = string.Empty;
    public virtual string ColorInfiniPinkDark { get; init; } = string.Empty;
    public virtual string ColorInfiniPinkLight { get; init; } = string.Empty;
    public virtual string ColorInfiniPurple { get; init; } = string.Empty;
    public virtual string ColorInfiniPurpleDark { get; init; } = string.Empty;
    public virtual string ColorInfiniPurpleLight { get; init; } = string.Empty;
    public virtual string ColorInfiniRed { get; init; } = string.Empty;
    public virtual string ColorInfiniRedDark { get; init; } = string.Empty;
    public virtual string ColorInfiniRedLight { get; init; } = string.Empty;
    public virtual string ColorInfiniWhite { get; init; } = string.Empty;
    public virtual string ColorInfiniYellow { get; init; } = string.Empty;
    public virtual string ColorInfiniYellowDark { get; init; } = string.Empty;
    public virtual string ColorInfiniYellowLight { get; init; } = string.Empty;
    public virtual string ColorInfiniEditorHeader { get; init; } = string.Empty;
    public virtual string ColorInfiniEditorHeaderBorder { get; init; } = string.Empty;
    public virtual string ColorInfiniPage { get; init; } = string.Empty;
    public virtual string ColorInfiniPageText { get; init; } = string.Empty;
    public virtual string ColorInfiniSection { get; init; } = string.Empty;
    public virtual string ColorInfiniSectionBorder { get; init; } = string.Empty;
    public virtual string ColorInfiniNavHamburger { get; init; } = string.Empty;
    public virtual string ColorInfiniNav { get; init; } = string.Empty;
    public virtual string ColorInfiniNavHorizontal { get; init; } = string.Empty;
    public virtual string ColorInfiniNavVertical { get; init; } = string.Empty;
    public virtual string ColorInfiniNavLeft { get; init; } = string.Empty;
    public virtual string ColorInfiniNavRight { get; init; } = string.Empty;
    public virtual string ColorInfiniNavBottom { get; init; } = string.Empty;
    public virtual string ColorInfiniNavTop { get; init; } = string.Empty;
    public virtual string ColorInfiniNavBorder { get; init; } = string.Empty;
    public virtual string ColorInfiniNavButton { get; init; } = string.Empty;
    public virtual string ColorInfiniNavButtonHover { get; init; } = string.Empty;
    public virtual string ColorInfiniNavHover { get; init; } = string.Empty;
    public virtual string ColorInfiniNavText { get; init; } = string.Empty;
    public virtual string ColorInfiniNavTextHover { get; init; } = string.Empty;
    public virtual string ColorInfiniTableHeader { get; init; } = string.Empty;
    public virtual string ColorInfiniTableRow { get; init; } = string.Empty;
    public virtual string ColorInfiniTableRowHover { get; init; } = string.Empty;
    public virtual string ColorInfiniText { get; init; } = string.Empty;
    public virtual string ColorInfiniTextError { get; init; } = string.Empty;
    public virtual string ColorInfiniToggleContainer { get; init; } = string.Empty;
    public virtual string ColorInfiniToggleIconOff { get; init; } = string.Empty;
    public virtual string ColorInfiniToggleIconOn { get; init; } = string.Empty;
    public virtual string ColorInfiniToggleSlider { get; init; } = string.Empty;
    public virtual string Transparent { get; init; } = string.Empty;
    public virtual string ColorInfiniUsericonAlt { get; init; } = string.Empty;
    public virtual string ColorInfiniUsericonAltHover { get; init; } = string.Empty;
    public virtual string ColorInfiniUsericonRing { get; init; } = string.Empty;
    public virtual string ColorInfiniUsericonStatus { get; init; } = string.Empty;
    public virtual string ColorInfiniCodeblock { get; init; } = string.Empty;

    public virtual string ColorInfiniButtonOutlineSolid { get;  init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineSolidText { get;  init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineSolidHover { get;  init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineSolidTextHover { get;  init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineSolidRing { get;  init; } = string.Empty;
    public virtual string ColorInfiniButtonOutlineSolidRingHover { get;  init; } = string.Empty;
}
