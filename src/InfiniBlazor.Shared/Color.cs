// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Color {
    Default,
    Accent,
    Red,
    Orange,
    Yellow,
    Green,
    Cyan,
    Blue,
    Purple,
    Pink,
    Gray,
    White,
    Black
}

public static class ColorExtensions {
    #region Text
    public static string ToCssClassText(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "text-infini-accent",
        Color.Red => "text-infini-red",
        Color.Orange => "text-infini-orange",
        Color.Yellow => "text-infini-yellow",
        Color.Green => "text-infini-green",
        Color.Cyan => "text-infini-cyan",
        Color.Blue => "text-infini-blue",
        Color.Purple => "text-infini-purple",
        Color.Pink => "text-infini-pink",
        Color.Gray => "text-infini-gray",
        Color.White => "text-infini-white",
        Color.Black => "text-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassTextColorReadableForeground(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "text-infini-black",
        Color.Red => "text-infini-white",
        Color.Orange => "text-infini-black",
        Color.Yellow => "text-infini-black",
        Color.Green => "text-infini-white",
        Color.Cyan => "text-infini-white",
        Color.Blue => "text-infini-white",
        Color.Purple => "text-infini-white",
        Color.Pink => "text-infini-white",
        Color.Gray => "text-infini-white",
        Color.White => "text-infini-black",
        Color.Black => "text-infini-white",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Background
    public static string ToCssClassBackground(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "overload-bg-infini-accent",
        Color.Red => "overload-bg-infini-red",
        Color.Orange => "overload-bg-infini-orange",
        Color.Yellow => "overload-bg-infini-yellow",
        Color.Green => "overload-bg-infini-green",
        Color.Cyan => "overload-bg-infini-cyan",
        Color.Blue => "overload-bg-infini-blue",
        Color.Purple => "overload-bg-infini-purple",
        Color.Pink => "overload-bg-infini-pink",
        Color.Gray => "overload-bg-infini-gray",
        Color.White => "overload-bg-infini-white",
        Color.Black => "overload-bg-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBackgroundStripes(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "bg-stripes bg-stripes-infini-accent",
        Color.Red => "bg-stripes bg-stripes-infini-red",
        Color.Orange => "bg-stripes bg-stripes-infini-orange",
        Color.Yellow => "bg-stripes bg-stripes-infini-yellow",
        Color.Green => "bg-stripes bg-stripes-infini-green",
        Color.Cyan => "bg-stripes bg-stripes-infini-cyan",
        Color.Blue => "bg-stripes bg-stripes-infini-blue",
        Color.Purple => "bg-stripes bg-stripes-infini-purple",
        Color.Pink => "bg-stripes bg-stripes-infini-pink",
        Color.Gray => "bg-stripes bg-stripes-infini-gray",
        Color.White => "bg-stripes bg-stripes-infini-white",
        Color.Black => "bg-stripes bg-stripes-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Ring
    public static string ToCssClassRing(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "ring-infini-accent",
        Color.Red => "ring-infini-red",
        Color.Orange => "ring-infini-orange",
        Color.Yellow => "ring-infini-yellow",
        Color.Green => "ring-infini-green",
        Color.Cyan => "ring-infini-cyan",
        Color.Blue => "ring-infini-blue",
        Color.Purple => "ring-infini-purple",
        Color.Pink => "ring-infini-pink",
        Color.Gray => "ring-infini-gray",
        Color.White => "ring-infini-white",
        Color.Black => "ring-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassRingHover(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "hover:ring-infini-accent",
        Color.Red => "hover:ring-infini-red",
        Color.Orange => "hover:ring-infini-orange",
        Color.Yellow => "hover:ring-infini-yellow",
        Color.Green => "hover:ring-infini-green",
        Color.Cyan => "hover:ring-infini-cyan",
        Color.Blue => "hover:ring-infini-blue",
        Color.Purple => "hover:ring-infini-purple",
        Color.Pink => "hover:ring-infini-pink",
        Color.Gray => "hover:ring-infini-gray",
        Color.White => "hover:ring-infini-white",
        Color.Black => "hover:ring-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Border
    public static string ToCssClassBorder(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border border-infini-accent",
        Color.Red => "border border-infini-red",
        Color.Orange => "border border-infini-orange",
        Color.Yellow => "border border-infini-yellow",
        Color.Green => "border border-infini-green",
        Color.Cyan => "border border-infini-cyan",
        Color.Blue => "border border-infini-blue",
        Color.Purple => "border border-infini-purple",
        Color.Pink => "border border-infini-pink",
        Color.Gray => "border border-infini-gray",
        Color.White => "border border-infini-white",
        Color.Black => "border border-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderTop(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-t border-t-infini-accent",
        Color.Red => "border-t border-t-infini-red",
        Color.Orange => "border-t border-t-infini-orange",
        Color.Yellow => "border-t border-t-infini-yellow",
        Color.Green => "border-t border-t-infini-green",
        Color.Cyan => "border-t border-t-infini-cyan",
        Color.Blue => "border-t border-t-infini-blue",
        Color.Purple => "border-t border-t-infini-purple",
        Color.Pink => "border-t border-t-infini-pink",
        Color.Gray => "border-t border-t-infini-gray",
        Color.White => "border-t border-t-infini-white",
        Color.Black => "border-t border-t-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderRight(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-r border-r-infini-accent",
        Color.Red => "border-r border-r-infini-red",
        Color.Orange => "border-r border-r-infini-orange",
        Color.Yellow => "border-r border-r-infini-yellow",
        Color.Green => "border-r border-r-infini-green",
        Color.Cyan => "border-r border-r-infini-cyan",
        Color.Blue => "border-r border-r-infini-blue",
        Color.Purple => "border-r border-r-infini-purple",
        Color.Pink => "border-r border-r-infini-pink",
        Color.Gray => "border-r border-r-infini-gray",
        Color.White => "border-r border-r-infini-white",
        Color.Black => "border-r border-r-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderBottom(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-b border-b-infini-accent",
        Color.Red => "border-b border-b-infini-red",
        Color.Orange => "border-b border-b-infini-orange",
        Color.Yellow => "border-b border-b-infini-yellow",
        Color.Green => "border-b border-b-infini-green",
        Color.Cyan => "border-b border-b-infini-cyan",
        Color.Blue => "border-b border-b-infini-blue",
        Color.Purple => "border-b border-b-infini-purple",
        Color.Pink => "border-b border-b-infini-pink",
        Color.Gray => "border-b border-b-infini-gray",
        Color.White => "border-b border-b-infini-white",
        Color.Black => "border-b border-b-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderLeft(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-l border-l-infini-accent",
        Color.Red => "border-l border-l-infini-red",
        Color.Orange => "border-l border-l-infini-orange",
        Color.Yellow => "border-l border-l-infini-yellow",
        Color.Green => "border-l border-l-infini-green",
        Color.Cyan => "border-l border-l-infini-cyan",
        Color.Blue => "border-l border-l-infini-blue",
        Color.Purple => "border-l border-l-infini-purple",
        Color.Pink => "border-l border-l-infini-pink",
        Color.Gray => "border-l border-l-infini-gray",
        Color.White => "border-l border-l-infini-white",
        Color.Black => "border-l border-l-infini-black",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
}
