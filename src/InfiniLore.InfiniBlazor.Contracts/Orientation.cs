// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Orientation {
    Horizontal,
    Vertical
}

public static class OrientationExtensions {
    public static string ToFlex(this Orientation orientation) => orientation switch {
        Orientation.Horizontal => "flex-row",
        Orientation.Vertical => "flex-col",
        _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
    };
    
    public static string ToFull(this Orientation orientation) => orientation switch {
        Orientation.Horizontal => "w-full",
        Orientation.Vertical => "h-full",
        _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
    };
}