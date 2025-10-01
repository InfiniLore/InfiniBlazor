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

    public virtual string Border { get; init; } = string.Empty;
    public virtual string ButtonDark { get; init; } = string.Empty;
    public virtual string ButtonDarkHover { get; init; } = string.Empty;
    public virtual string ButtonDarkRingHover { get; init; } = string.Empty;
    public virtual string ButtonDarkText { get; init; } = string.Empty;
    public virtual string ButtonDarkTextHover { get; init; } = string.Empty;
    public virtual string ButtonDefault { get; init; } = string.Empty;
    public virtual string ButtonDefaultHover { get; init; } = string.Empty;
    public virtual string ButtonDefaultText { get; init; } = string.Empty;
    public virtual string ButtonDefaultTextHover { get; init; } = string.Empty;
    public virtual string ButtonDisabled { get; init; } = string.Empty;
    public virtual string ButtonDisabledText { get; init; } = string.Empty;
    public virtual string ButtonLight { get; init; } = string.Empty;
    public virtual string ButtonLightHover { get; init; } = string.Empty;
    public virtual string ButtonLightRingHover { get; init; } = string.Empty;
    public virtual string ButtonLightText { get; init; } = string.Empty;
    public virtual string ButtonLightTextHover { get; init; } = string.Empty;
    public virtual string ButtonOutline { get; init; } = string.Empty;
    public virtual string ButtonOutlineHover { get; init; } = string.Empty;
    public virtual string ButtonOutlineRing { get; init; } = string.Empty;
    public virtual string ButtonOutlineRingHover { get; init; } = string.Empty;
    public virtual string ButtonOutlineText { get; init; } = string.Empty;
    public virtual string ButtonOutlineTextHover { get; init; } = string.Empty;
    public virtual string ButtonPrimary { get; init; } = string.Empty;
    public virtual string ButtonPrimaryHover { get; init; } = string.Empty;
    public virtual string ButtonPrimaryText { get; init; } = string.Empty;
    public virtual string ButtonPrimaryTextHover { get; init; } = string.Empty;
    public virtual string ButtonTransparent { get; init; } = string.Empty;
    public virtual string ButtonTransparentHover { get; init; } = string.Empty;
    public virtual string ButtonTransparentText { get; init; } = string.Empty;
    public virtual string ButtonTransparentTextHover { get; init; } = string.Empty;
    public virtual string ColorAccent { get; init; } = string.Empty;
    public virtual string ColorAccentDark { get; init; } = string.Empty;
    public virtual string ColorAccentLight { get; init; } = string.Empty;
    public virtual string ColorBase00 { get; init; } = string.Empty;
    public virtual string ColorBase05 { get; init; } = string.Empty;
    public virtual string ColorBase10 { get; init; } = string.Empty;
    public virtual string ColorBase100 { get; init; } = string.Empty;
    public virtual string ColorBase20 { get; init; } = string.Empty;
    public virtual string ColorBase30 { get; init; } = string.Empty;
    public virtual string ColorBase40 { get; init; } = string.Empty;
    public virtual string ColorBase50 { get; init; } = string.Empty;
    public virtual string ColorBase60 { get; init; } = string.Empty;
    public virtual string ColorBase70 { get; init; } = string.Empty;
    public virtual string ColorBase80 { get; init; } = string.Empty;
    public virtual string ColorBase90 { get; init; } = string.Empty;
    public virtual string ColorBase95 { get; init; } = string.Empty;
    public virtual string ColorBlack { get; init; } = string.Empty;
    public virtual string ColorBlue { get; init; } = string.Empty;
    public virtual string ColorBlueDark { get; init; } = string.Empty;
    public virtual string ColorBlueLight { get; init; } = string.Empty;
    public virtual string ColorCyan { get; init; } = string.Empty;
    public virtual string ColorCyanDark { get; init; } = string.Empty;
    public virtual string ColorCyanLight { get; init; } = string.Empty;
    public virtual string ColorGray { get; init; } = string.Empty;
    public virtual string ColorGrayDark { get; init; } = string.Empty;
    public virtual string ColorGrayLight { get; init; } = string.Empty;
    public virtual string ColorGreen { get; init; } = string.Empty;
    public virtual string ColorGreenDark { get; init; } = string.Empty;
    public virtual string ColorGreenLight { get; init; } = string.Empty;
    public virtual string ColorOrange { get; init; } = string.Empty;
    public virtual string ColorOrangeDark { get; init; } = string.Empty;
    public virtual string ColorOrangeLight { get; init; } = string.Empty;
    public virtual string ColorPink { get; init; } = string.Empty;
    public virtual string ColorPinkDark { get; init; } = string.Empty;
    public virtual string ColorPinkLight { get; init; } = string.Empty;
    public virtual string ColorPurple { get; init; } = string.Empty;
    public virtual string ColorPurpleDark { get; init; } = string.Empty;
    public virtual string ColorPurpleLight { get; init; } = string.Empty;
    public virtual string ColorRed { get; init; } = string.Empty;
    public virtual string ColorRedDark { get; init; } = string.Empty;
    public virtual string ColorRedLight { get; init; } = string.Empty;
    public virtual string ColorWhite { get; init; } = string.Empty;
    public virtual string ColorYellow { get; init; } = string.Empty;
    public virtual string ColorYellowDark { get; init; } = string.Empty;
    public virtual string ColorYellowLight { get; init; } = string.Empty;
    public virtual string EditorHeader { get; init; } = string.Empty;
    public virtual string EditorHeaderBorder { get; init; } = string.Empty;
    public virtual string Page { get; init; } = string.Empty;
    public virtual string PageText { get; init; } = string.Empty;
    public virtual string Section { get; init; } = string.Empty;
    public virtual string SectionBorder { get; init; } = string.Empty;
    public virtual string NavHamburger { get; init; } = string.Empty;
    public virtual string Nav { get; init; } = string.Empty;
    public virtual string NavHorizontal { get; init; } = string.Empty;
    public virtual string NavVertical { get; init; } = string.Empty;
    public virtual string NavLeft { get; init; } = string.Empty;
    public virtual string NavRight { get; init; } = string.Empty;
    public virtual string NavBottom { get; init; } = string.Empty;
    public virtual string NavTop { get; init; } = string.Empty;
    public virtual string NavBorder { get; init; } = string.Empty;
    public virtual string NavButton { get; init; } = string.Empty;
    public virtual string NavButtonHover { get; init; } = string.Empty;
    public virtual string NavHover { get; init; } = string.Empty;
    public virtual string NavText { get; init; } = string.Empty;
    public virtual string NavTextHover { get; init; } = string.Empty;
    public virtual string TableHeader { get; init; } = string.Empty;
    public virtual string TableRow { get; init; } = string.Empty;
    public virtual string TableRowHover { get; init; } = string.Empty;
    public virtual string Text { get; init; } = string.Empty;
    public virtual string TextError { get; init; } = string.Empty;
    public virtual string ToggleContainer { get; init; } = string.Empty;
    public virtual string ToggleIconOff { get; init; } = string.Empty;
    public virtual string ToggleIconOn { get; init; } = string.Empty;
    public virtual string ToggleSlider { get; init; } = string.Empty;
    public virtual string Transparent { get; init; } = string.Empty;
    public virtual string UsericonAlt { get; init; } = string.Empty;
    public virtual string UsericonAltHover { get; init; } = string.Empty;
    public virtual string UsericonRing { get; init; } = string.Empty;
    public virtual string UsericonStatus { get; init; } = string.Empty;
    public virtual string Codeblock { get; init; } = string.Empty;

    public virtual string ButtonOutlineSolid { get;  init; } = string.Empty;
    public virtual string ButtonOutlineSolidText { get;  init; } = string.Empty;
    public virtual string ButtonOutlineSolidHover { get;  init; } = string.Empty;
    public virtual string ButtonOutlineSolidTextHover { get;  init; } = string.Empty;
    public virtual string ButtonOutlineSolidRing { get;  init; } = string.Empty;
    public virtual string ButtonOutlineSolidRingHover { get;  init; } = string.Empty;
}
