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
    public static string ToReadableTextColorTailwind(this Color color) 
        => color switch {
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
    
    public static string ToRingTailwind(this Color color) 
        => color switch {
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

    public static string ToBackgroundTailwind(this Color color)
        => color switch {
            Color.Default => string.Empty,
            Color.Accent => "bgc-(--color-accent)",
            Color.Red => "bgc-(--color-red)",
            Color.Orange => "bgc-(--color-orange)",
            Color.Yellow => "bgc-(--color-yellow)",
            Color.Green => "bgc-(--color-green)",
            Color.Cyan => "bgc-(--color-cyan)",
            Color.Blue => "bgc-(--color-blue)",
            Color.Purple => "bgc-(--color-purple)",
            Color.Pink => "bgc-(--color-pink)",
            Color.Gray => "bgc-(--color-gray)",
            Color.White => "bgc-(--color-white)",
            Color.Black => "bgc-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public static string ToTextTailwind(this Color color) 
        => color switch {
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
    
    public static string ToBorderTailwind(this Color color) 
        => color switch {
            Color.Default => string.Empty,
            Color.Accent => "border-(--color-accent)",
            Color.Red => "border-(--color-red)",
            Color.Orange => "border-(--color-orange)",
            Color.Yellow => "border-(--color-yellow)",
            Color.Green => "border-(--color-green)",
            Color.Cyan => "border-(--color-cyan)",
            Color.Blue => "border-(--color-blue)",
            Color.Purple => "border-(--color-purple)",
            Color.Pink => "border-(--color-pink)",
            Color.Gray => "border-(--color-gray)",
            Color.White => "border-(--color-white)",
            Color.Black => "border-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public static string ToHoverRingTailwind(this Color color) 
        => color switch {
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
}
