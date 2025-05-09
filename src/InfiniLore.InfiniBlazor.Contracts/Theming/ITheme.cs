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
    #region Colors
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorRedRgb { get; }
    [IncludeAsCssVariable]
    string ColorRed { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorOrangeRgb { get; }
    [IncludeAsCssVariable]
    string ColorOrange { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorYellowRgb { get; }
    [IncludeAsCssVariable]
    string ColorYellow { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorGreenRgb { get; }
    [IncludeAsCssVariable]
    string ColorGreen { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorCyanRgb { get; }
    [IncludeAsCssVariable]
    string ColorCyan { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBlueRgb { get; }
    [IncludeAsCssVariable]
    string ColorBlue { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorPurpleRgb { get; }
    [IncludeAsCssVariable]
    string ColorPurple { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorPinkRgb { get; }
    [IncludeAsCssVariable]
    string ColorPink { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorGrayRgb { get; }
    [IncludeAsCssVariable]
    string ColorGray { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorWhiteRgb { get; }
    [IncludeAsCssVariable]
    string ColorWhite { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBlackRgb { get; }
    [IncludeAsCssVariable]
    string ColorBlack { get; init; }

    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorAccentRgb { get; }
    [IncludeAsCssVariable]
    string ColorAccent { get; init; }

    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase00Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase00 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase05Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase05 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase10Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase10 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase20Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase20 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase30Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase30 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase40Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase40 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase50Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase50 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase60Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase60 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase70Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase70 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase80Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase80 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase90Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase90 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase95Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase95 { get; init; }
    [IncludeAsCssVariable] [InterpretAsRgb]
    string ColorBase100Rgb { get; }
    [IncludeAsCssVariable]
    string ColorBase100 { get; init; }
    #endregion
    #endregion
}
