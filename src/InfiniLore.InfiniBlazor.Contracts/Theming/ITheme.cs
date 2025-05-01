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
    [IncludeAsCssVariable] string ColorRedRgb { get; }
    [IncludeAsCssVariable] string ColorRed { get; init; }
    [IncludeAsCssVariable] string ColorOrangeRgb { get; }
    [IncludeAsCssVariable] string ColorOrange { get; init; }
    [IncludeAsCssVariable] string ColorYellowRgb { get; }
    [IncludeAsCssVariable] string ColorYellow { get; init; }
    [IncludeAsCssVariable] string ColorGreenRgb { get; }
    [IncludeAsCssVariable] string ColorGreen { get; init; }
    [IncludeAsCssVariable] string ColorCyanRgb { get; }
    [IncludeAsCssVariable] string ColorCyan { get; init; }
    [IncludeAsCssVariable] string ColorBlueRgb { get; }
    [IncludeAsCssVariable] string ColorBlue { get; init; }
    [IncludeAsCssVariable] string ColorPurpleRgb { get; }
    [IncludeAsCssVariable] string ColorPurple { get; init; }
    [IncludeAsCssVariable] string ColorPinkRgb { get; }
    [IncludeAsCssVariable] string ColorPink { get; init; }
    
    [IncludeAsCssVariable] string ColorAccentRgb { get; }
    [IncludeAsCssVariable] string ColorAccent { get; init; }

    [IncludeAsCssVariable] string ColorBase00Rgb { get; }
    [IncludeAsCssVariable] string ColorBase00 { get; init; }
    [IncludeAsCssVariable] string ColorBase05Rgb { get; }
    [IncludeAsCssVariable] string ColorBase05 { get; init; }
    [IncludeAsCssVariable] string ColorBase10Rgb { get; }
    [IncludeAsCssVariable] string ColorBase10 { get; init; }
    [IncludeAsCssVariable] string ColorBase20Rgb { get; }
    [IncludeAsCssVariable] string ColorBase20 { get; init; }
    [IncludeAsCssVariable] string ColorBase30Rgb { get; }
    [IncludeAsCssVariable] string ColorBase30 { get; init; }
    [IncludeAsCssVariable] string ColorBase40Rgb { get; }
    [IncludeAsCssVariable] string ColorBase40 { get; init; }
    [IncludeAsCssVariable] string ColorBase50Rgb { get; }
    [IncludeAsCssVariable] string ColorBase50 { get; init; }
    [IncludeAsCssVariable] string ColorBase60Rgb { get; }
    [IncludeAsCssVariable] string ColorBase60 { get; init; }
    [IncludeAsCssVariable] string ColorBase70Rgb { get; }
    [IncludeAsCssVariable] string ColorBase70 { get; init; }
    [IncludeAsCssVariable] string ColorBase80Rgb { get; }
    [IncludeAsCssVariable] string ColorBase80 { get; init; }
    [IncludeAsCssVariable] string ColorBase90Rgb { get; }
    [IncludeAsCssVariable] string ColorBase90 { get; init; }
    [IncludeAsCssVariable] string ColorBase95Rgb { get; }
    [IncludeAsCssVariable] string ColorBase95 { get; init; }
    [IncludeAsCssVariable] string ColorBase100Rgb { get; }
    [IncludeAsCssVariable] string ColorBase100 { get; init; }
    #endregion
    #endregion
}
