// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorTheme : ITheme {
    public InfiniBlazorTheme() {}
    public InfiniBlazorTheme(string colorAccent) {
        ColorAccent = colorAccent;
    }
    public IDictionary<string, string> ToDictionary() => throw new NotImplementedException();
    public static ITheme Instance { get; } = new InfiniBlazorTheme();

    #region CssData
    #region Colors
    public virtual string ColorRedRgb => ColorRed.ConvertToRgbValues();
    public virtual string ColorRed { get; init; } = "#ef4444";
    public virtual string ColorOrangeRgb => ColorOrange.ConvertToRgbValues();
    public virtual string ColorOrange { get; init; } = "#f97316";
    public virtual string ColorYellowRgb => ColorYellow.ConvertToRgbValues();
    public virtual string ColorYellow { get; init; } = "#eab308";
    public virtual string ColorGreenRgb => ColorGreen.ConvertToRgbValues();
    public virtual string ColorGreen { get; init; } = "#22c55e";
    public virtual string ColorCyanRgb => ColorCyan.ConvertToRgbValues();
    public virtual string ColorCyan { get; init; } = "#06b6d4";
    public virtual string ColorBlueRgb => ColorBlue.ConvertToRgbValues();
    public virtual string ColorBlue { get; init; } = "#3b82f6";
    public virtual string ColorPurpleRgb => ColorPurple.ConvertToRgbValues();
    public virtual string ColorPurple { get; init; } = "#a855f7";
    public virtual string ColorPinkRgb => ColorPink.ConvertToRgbValues();
    public virtual string ColorPink { get; init; } = "#ec4899";
    public virtual string ColorAccentRgb => ColorAccent.ConvertToRgbValues();
    public virtual string ColorAccent { get; init; } = "#3b82f6";

    public virtual string ColorBase00Rgb => ColorBase00.ConvertToRgbValues();
    public virtual string ColorBase00 { get; init; } = "#ffffff";
    public virtual string ColorBase05Rgb => ColorBase05.ConvertToRgbValues();
    public virtual string ColorBase05 { get; init; } = "#f6f7f7";
    public virtual string ColorBase10Rgb => ColorBase10.ConvertToRgbValues();
    public virtual string ColorBase10 { get; init; } = "#e1e6e4";
    public virtual string ColorBase20Rgb => ColorBase20.ConvertToRgbValues();
    public virtual string ColorBase20 { get; init; } = "#c3ccc9";
    public virtual string ColorBase30Rgb => ColorBase30.ConvertToRgbValues();
    public virtual string ColorBase30 { get; init; } = "#9caca5";
    public virtual string ColorBase40Rgb => ColorBase40.ConvertToRgbValues();
    public virtual string ColorBase40 { get; init; } = "#788982";
    public virtual string ColorBase50Rgb => ColorBase50.ConvertToRgbValues();
    public virtual string ColorBase50 { get; init; } = "#5d6f68";
    public virtual string ColorBase60Rgb => ColorBase60.ConvertToRgbValues();
    public virtual string ColorBase60 { get; init; } = "#495852";
    public virtual string ColorBase70Rgb => ColorBase70.ConvertToRgbValues();
    public virtual string ColorBase70 { get; init; } = "#3d4844";
    public virtual string ColorBase80Rgb => ColorBase80.ConvertToRgbValues();
    public virtual string ColorBase80 { get; init; } = "#333c39";
    public virtual string ColorBase90Rgb => ColorBase90.ConvertToRgbValues();
    public virtual string ColorBase90 { get; init; } = "#2b312f";
    public virtual string ColorBase95Rgb => ColorBase95.ConvertToRgbValues();
    public virtual string ColorBase95 { get; init; } = "#171c1b";
    public virtual string ColorBase100Rgb => ColorBase100.ConvertToRgbValues();
    public virtual string ColorBase100 { get; init; } = "#000000";
    #endregion
    #endregion
}
