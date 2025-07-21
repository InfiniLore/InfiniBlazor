// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;
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
        Color.Accent => "text-(--color-accent)",
        Color.Red => "text-(--color-red)",
        Color.Orange => "text-(--color-orange)",
        Color.Yellow => "text-(--color-yellow)",
        Color.Green => "text-(--color-green)",
        Color.Cyan => "text-(--color-cyan)",
        Color.Blue => "text-(--color-blue)",
        Color.Purple => "text-(--color-purple)",
        Color.Pink => "text-(--color-pink)",
        Color.Gray => "text-(--color-gray)",
        Color.White => "text-(--color-white)",
        Color.Black => "text-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassTextColorReadableForeground(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "text-(--color-black)",
        Color.Red => "text-(--color-white)",
        Color.Orange => "text-(--color-black)",
        Color.Yellow => "text-(--color-black)",
        Color.Green => "text-(--color-white)",
        Color.Cyan => "text-(--color-white)",
        Color.Blue => "text-(--color-white)",
        Color.Purple => "text-(--color-white)",
        Color.Pink => "text-(--color-white)",
        Color.Gray => "text-(--color-white)",
        Color.White => "text-(--color-black)",
        Color.Black => "text-(--color-white)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Background
    public static string ToCssClassBackground(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "infini-bg-(--color-accent)",
        Color.Red => "infini-bg-(--color-red)",
        Color.Orange => "infini-bg-(--color-orange)",
        Color.Yellow => "infini-bg-(--color-yellow)",
        Color.Green => "infini-bg-(--color-green)",
        Color.Cyan => "infini-bg-(--color-cyan)",
        Color.Blue => "infini-bg-(--color-blue)",
        Color.Purple => "infini-bg-(--color-purple)",
        Color.Pink => "infini-bg-(--color-pink)",
        Color.Gray => "infini-bg-(--color-gray)",
        Color.White => "infini-bg-(--color-white)",
        Color.Black => "infini-bg-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBackgroundStripes(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "bg-stripes bg-stripes-(--color-accent)",
        Color.Red => "bg-stripes bg-stripes-(--color-red)",
        Color.Orange => "bg-stripes bg-stripes-(--color-orange)",
        Color.Yellow => "bg-stripes bg-stripes-(--color-yellow)",
        Color.Green => "bg-stripes bg-stripes-(--color-green)",
        Color.Cyan => "bg-stripes bg-stripes-(--color-cyan)",
        Color.Blue => "bg-stripes bg-stripes-(--color-blue)",
        Color.Purple => "bg-stripes bg-stripes-(--color-purple)",
        Color.Pink => "bg-stripes bg-stripes-(--color-pink)",
        Color.Gray => "bg-stripes bg-stripes-(--color-gray)",
        Color.White => "bg-stripes bg-stripes-(--color-white)",
        Color.Black => "bg-stripes bg-stripes-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Ring
    public static string ToCssClassRing(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "ring-(--color-accent)",
        Color.Red => "ring-(--color-red)",
        Color.Orange => "ring-(--color-orange)",
        Color.Yellow => "ring-(--color-yellow)",
        Color.Green => "ring-(--color-green)",
        Color.Cyan => "ring-(--color-cyan)",
        Color.Blue => "ring-(--color-blue)",
        Color.Purple => "ring-(--color-purple)",
        Color.Pink => "ring-(--color-pink)",
        Color.Gray => "ring-(--color-gray)",
        Color.White => "ring-(--color-white)",
        Color.Black => "ring-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassRingHover(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "hover:ring-(--color-accent)",
        Color.Red => "hover:ring-(--color-red)",
        Color.Orange => "hover:ring-(--color-orange)",
        Color.Yellow => "hover:ring-(--color-yellow)",
        Color.Green => "hover:ring-(--color-green)",
        Color.Cyan => "hover:ring-(--color-cyan)",
        Color.Blue => "hover:ring-(--color-blue)",
        Color.Purple => "hover:ring-(--color-purple)",
        Color.Pink => "hover:ring-(--color-pink)",
        Color.Gray => "hover:ring-(--color-gray)",
        Color.White => "hover:ring-(--color-white)",
        Color.Black => "hover:ring-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
    #region Border
    public static string ToCssClassBorder(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border border-(--color-accent)",
        Color.Red => "border border-(--color-red)",
        Color.Orange => "border border-(--color-orange)",
        Color.Yellow => "border border-(--color-yellow)",
        Color.Green => "border border-(--color-green)",
        Color.Cyan => "border border-(--color-cyan)",
        Color.Blue => "border border-(--color-blue)",
        Color.Purple => "border border-(--color-purple)",
        Color.Pink => "border border-(--color-pink)",
        Color.Gray => "border border-(--color-gray)",
        Color.White => "border border-(--color-white)",
        Color.Black => "border border-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderTop(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-t border-t-(--color-accent)",
        Color.Red => "border-t border-t-(--color-red)",
        Color.Orange => "border-t border-t-(--color-orange)",
        Color.Yellow => "border-t border-t-(--color-yellow)",
        Color.Green => "border-t border-t-(--color-green)",
        Color.Cyan => "border-t border-t-(--color-cyan)",
        Color.Blue => "border-t border-t-(--color-blue)",
        Color.Purple => "border-t border-t-(--color-purple)",
        Color.Pink => "border-t border-t-(--color-pink)",
        Color.Gray => "border-t border-t-(--color-gray)",
        Color.White => "border-t border-t-(--color-white)",
        Color.Black => "border-t border-t-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderRight(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-r border-r-(--color-accent)",
        Color.Red => "border-r border-r-(--color-red)",
        Color.Orange => "border-r border-r-(--color-orange)",
        Color.Yellow => "border-r border-r-(--color-yellow)",
        Color.Green => "border-r border-r-(--color-green)",
        Color.Cyan => "border-r border-r-(--color-cyan)",
        Color.Blue => "border-r border-r-(--color-blue)",
        Color.Purple => "border-r border-r-(--color-purple)",
        Color.Pink => "border-r border-r-(--color-pink)",
        Color.Gray => "border-r border-r-(--color-gray)",
        Color.White => "border-r border-r-(--color-white)",
        Color.Black => "border-r border-r-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderBottom(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-b border-b-(--color-accent)",
        Color.Red => "border-b border-b-(--color-red)",
        Color.Orange => "border-b border-b-(--color-orange)",
        Color.Yellow => "border-b border-b-(--color-yellow)",
        Color.Green => "border-b border-b-(--color-green)",
        Color.Cyan => "border-b border-b-(--color-cyan)",
        Color.Blue => "border-b border-b-(--color-blue)",
        Color.Purple => "border-b border-b-(--color-purple)",
        Color.Pink => "border-b border-b-(--color-pink)",
        Color.Gray => "border-b border-b-(--color-gray)",
        Color.White => "border-b border-b-(--color-white)",
        Color.Black => "border-b border-b-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };

    public static string ToCssClassBorderLeft(this Color color) => color switch {
        Color.Default => string.Empty,
        Color.Accent => "border-l border-l-(--color-accent)",
        Color.Red => "border-l border-l-(--color-red)",
        Color.Orange => "border-l border-l-(--color-orange)",
        Color.Yellow => "border-l border-l-(--color-yellow)",
        Color.Green => "border-l border-l-(--color-green)",
        Color.Cyan => "border-l border-l-(--color-cyan)",
        Color.Blue => "border-l border-l-(--color-blue)",
        Color.Purple => "border-l border-l-(--color-purple)",
        Color.Pink => "border-l border-l-(--color-pink)",
        Color.Gray => "border-l border-l-(--color-gray)",
        Color.White => "border-l border-l-(--color-white)",
        Color.Black => "border-l border-l-(--color-black)",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
    #endregion
}
