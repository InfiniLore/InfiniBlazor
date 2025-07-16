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

    public string Border { get; init; } = string.Empty;
    public string ButtonDark { get; init; } = string.Empty;
    public string ButtonDarkHover { get; init; } = string.Empty;
    public string ButtonDarkRingHover { get; init; } = string.Empty;
    public string ButtonDarkText { get; init; } = string.Empty;
    public string ButtonDarkTextHover { get; init; } = string.Empty;
    public string ButtonDefault { get; init; } = string.Empty;
    public string ButtonDefaultHover { get; init; } = string.Empty;
    public string ButtonDefaultText { get; init; } = string.Empty;
    public string ButtonDefaultTextHover { get; init; } = string.Empty;
    public string ButtonDisabled { get; init; } = string.Empty;
    public string ButtonDisabledText { get; init; } = string.Empty;
    public string ButtonLight { get; init; } = string.Empty;
    public string ButtonLightHover { get; init; } = string.Empty;
    public string ButtonLightRingHover { get; init; } = string.Empty;
    public string ButtonLightText { get; init; } = string.Empty;
    public string ButtonLightTextHover { get; init; } = string.Empty;
    public string ButtonOutline { get; init; } = string.Empty;
    public string ButtonOutlineHover { get; init; } = string.Empty;
    public string ButtonOutlineRing { get; init; } = string.Empty;
    public string ButtonOutlineRingHover { get; init; } = string.Empty;
    public string ButtonOutlineText { get; init; } = string.Empty;
    public string ButtonOutlineTextHover { get; init; } = string.Empty;
    public string ButtonPrimary { get; init; } = string.Empty;
    public string ButtonPrimaryHover { get; init; } = string.Empty;
    public string ButtonPrimaryText { get; init; } = string.Empty;
    public string ButtonPrimaryTextHover { get; init; } = string.Empty;
    public string ButtonTransparent { get; init; } = string.Empty;
    public string ButtonTransparentHover { get; init; } = string.Empty;
    public string ButtonTransparentText { get; init; } = string.Empty;
    public string ButtonTransparentTextHover { get; init; } = string.Empty;
    public string ColorAccent { get; init; } = string.Empty;
    public string ColorAccentDark { get; init; } = string.Empty;
    public string ColorAccentLight { get; init; } = string.Empty;
    public string ColorBase00 { get; init; } = string.Empty;
    public string ColorBase05 { get; init; } = string.Empty;
    public string ColorBase10 { get; init; } = string.Empty;
    public string ColorBase100 { get; init; } = string.Empty;
    public string ColorBase20 { get; init; } = string.Empty;
    public string ColorBase30 { get; init; } = string.Empty;
    public string ColorBase40 { get; init; } = string.Empty;
    public string ColorBase50 { get; init; } = string.Empty;
    public string ColorBase60 { get; init; } = string.Empty;
    public string ColorBase70 { get; init; } = string.Empty;
    public string ColorBase80 { get; init; } = string.Empty;
    public string ColorBase90 { get; init; } = string.Empty;
    public string ColorBase95 { get; init; } = string.Empty;
    public string ColorBlack { get; init; } = string.Empty;
    public string ColorBlue { get; init; } = string.Empty;
    public string ColorBlueDark { get; init; } = string.Empty;
    public string ColorBlueLight { get; init; } = string.Empty;
    public string ColorCyan { get; init; } = string.Empty;
    public string ColorCyanDark { get; init; } = string.Empty;
    public string ColorCyanLight { get; init; } = string.Empty;
    public string ColorGray { get; init; } = string.Empty;
    public string ColorGrayDark { get; init; } = string.Empty;
    public string ColorGrayLight { get; init; } = string.Empty;
    public string ColorGreen { get; init; } = string.Empty;
    public string ColorGreenDark { get; init; } = string.Empty;
    public string ColorGreenLight { get; init; } = string.Empty;
    public string ColorOrange { get; init; } = string.Empty;
    public string ColorOrangeDark { get; init; } = string.Empty;
    public string ColorOrangeLight { get; init; } = string.Empty;
    public string ColorPink { get; init; } = string.Empty;
    public string ColorPinkDark { get; init; } = string.Empty;
    public string ColorPinkLight { get; init; } = string.Empty;
    public string ColorPurple { get; init; } = string.Empty;
    public string ColorPurpleDark { get; init; } = string.Empty;
    public string ColorPurpleLight { get; init; } = string.Empty;
    public string ColorRed { get; init; } = string.Empty;
    public string ColorRedDark { get; init; } = string.Empty;
    public string ColorRedLight { get; init; } = string.Empty;
    public string ColorWhite { get; init; } = string.Empty;
    public string ColorYellow { get; init; } = string.Empty;
    public string ColorYellowDark { get; init; } = string.Empty;
    public string ColorYellowLight { get; init; } = string.Empty;
    public string EditorHeader { get; init; } = string.Empty;
    public string EditorHeaderBorder { get; init; } = string.Empty;
    public string Page { get; init; } = string.Empty;
    public string PageText { get; init; } = string.Empty;
    public string Section { get; init; } = string.Empty;
    public string SectionBorder { get; init; } = string.Empty;
    public string SidebarHamburger { get; init; } = string.Empty;
    public string SidebarNav { get; init; } = string.Empty;
    public string SidebarNavBorder { get; init; } = string.Empty;
    public string SidebarNavButton { get; init; } = string.Empty;
    public string SidebarNavButtonHover { get; init; } = string.Empty;
    public string SidebarNavHover { get; init; } = string.Empty;
    public string SidebarNavText { get; init; } = string.Empty;
    public string SidebarNavTextHover { get; init; } = string.Empty;
    public string TableHeader { get; init; } = string.Empty;
    public string TableRow { get; init; } = string.Empty;
    public string TableRowHover { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public string TextError { get; init; } = string.Empty;
    public string ToggleContainer { get; init; } = string.Empty;
    public string ToggleIconOff { get; init; } = string.Empty;
    public string ToggleIconOn { get; init; } = string.Empty;
    public string ToggleSlider { get; init; } = string.Empty;
    public string Transparent { get; init; } = string.Empty;
    public string UsericonAlt { get; init; } = string.Empty;
    public string UsericonAltHover { get; init; } = string.Empty;
    public string UsericonRing { get; init; } = string.Empty;
    public string UsericonStatus { get; init; } = string.Empty;
    public string Codeblock { get; init; } = string.Empty;
    
    public string ButtonOutlineSolid { get;  init; } = string.Empty;
    public string ButtonOutlineSolidText { get;  init; } = string.Empty;
    public string ButtonOutlineSolidHover { get;  init; } = string.Empty;
    public string ButtonOutlineSolidTextHover { get;  init; } = string.Empty;
    public string ButtonOutlineSolidRing { get;  init; } = string.Empty;
    public string ButtonOutlineSolidRingHover { get;  init; } = string.Empty;
}
