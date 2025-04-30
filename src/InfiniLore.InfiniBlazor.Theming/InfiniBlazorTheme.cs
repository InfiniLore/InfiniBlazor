// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateThemeSymbols]
public partial class InfiniBlazorTheme : ITheme {
    public static ITheme Instance { get; } = new InfiniBlazorTheme();
    
    #region Colors
    public virtual string ColorRed { get; init; } = "#ef4444";
    public virtual string ColorOrange { get; init; } = "#f97316";
    public virtual string ColorYellow { get; init; } = "#eab308";
    public virtual string ColorGreen { get; init; } = "#22c55e";
    public virtual string ColorCyan { get; init; } = "#06b6d4";
    public virtual string ColorBlue { get; init; } = "#3b82f6";
    public virtual string ColorPurple { get; init; } = "#a855f7";
    public virtual string ColorPink { get; init; } = "#ec4899";
    public virtual string ColorAccent { get; init; } = "#3b82f6";

    public virtual string ColorBase00 { get; init; } = "#ffffff";
    public virtual string ColorBase05 { get; init; } = "#f6f7f7";
    public virtual string ColorBase10 { get; init; } = "#e1e6e4";
    public virtual string ColorBase20 { get; init; } = "#c3ccc9";
    public virtual string ColorBase30 { get; init; } = "#9caca5";
    public virtual string ColorBase40 { get; init; } = "#788982";
    public virtual string ColorBase50 { get; init; } = "#5d6f68";
    public virtual string ColorBase60 { get; init; } = "#495852";
    public virtual string ColorBase70 { get; init; } = "#3d4844";
    public virtual string ColorBase80 { get; init; } = "#333c39";
    public virtual string ColorBase90 { get; init; } = "#2b312f";
    public virtual string ColorBase95 { get; init; } = "#171c1b";
    public virtual string ColorBase100 { get; init; } = "#000000";
    #endregion
}
