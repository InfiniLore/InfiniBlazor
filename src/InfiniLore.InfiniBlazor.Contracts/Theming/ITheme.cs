// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ITheme {
    IDictionary<string, string> ToDictionary();

    #region CssData
    
    #region Colors
    string ColorRedRgb { get; }
    string ColorRed { get; }
    string ColorOrangeRgb { get; }
    string ColorOrange { get; }
    string ColorYellowRgb { get; }
    string ColorYellow { get; }
    string ColorGreenRgb { get; }
    string ColorGreen { get; }
    string ColorCyanRgb { get; }
    string ColorCyan { get; }
    string ColorBlueRgb { get; }
    string ColorBlue { get; }
    string ColorPurpleRgb { get; }
    string ColorPurple { get; }
    string ColorPinkRgb { get; }
    string ColorPink { get; }
    
    string ColorAccentRgb { get; }
    string ColorAccent { get; }

    string ColorBase00Rgb { get; }
    string ColorBase00 { get; }
    string ColorBase05Rgb { get; }
    string ColorBase05 { get; }
    string ColorBase10Rgb { get; }
    string ColorBase10 { get; }
    string ColorBase20Rgb { get; }
    string ColorBase20 { get; }
    string ColorBase30Rgb { get; }
    string ColorBase30 { get; }
    string ColorBase40Rgb { get; }
    string ColorBase40 { get; }
    string ColorBase50Rgb { get; }
    string ColorBase50 { get; }
    string ColorBase60Rgb { get; }
    string ColorBase60 { get; }
    string ColorBase70Rgb { get; }
    string ColorBase70 { get; }
    string ColorBase80Rgb { get; }
    string ColorBase80 { get; }
    string ColorBase90Rgb { get; }
    string ColorBase90 { get; }
    string ColorBase95Rgb { get; }
    string ColorBase95 { get; }
    string ColorBase100Rgb { get; }
    string ColorBase100 { get; }
    #endregion
    #endregion
}
