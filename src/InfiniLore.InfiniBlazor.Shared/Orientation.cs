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
    public static string ToAriaOrientation(this Orientation orientation) => orientation switch {
        Orientation.Horizontal => "horizontal",
        Orientation.Vertical => "vertical",
        _ => "horizontal"
    };
    
    public static string ToFlexDirection(this Orientation orientation) => orientation switch {
        Orientation.Horizontal => "row",
        Orientation.Vertical => "column",
        _ => "row"
    };
    
    public static string ToSize(this Orientation orientation) => orientation switch {
        Orientation.Horizontal => "w-full",
        Orientation.Vertical => "h-full",
        _ => "w-full"
    };
    
    public static T Match<T>(this Orientation orientation, Func<T> onHorizontal, Func<T> onVertical) => orientation switch {
        Orientation.Horizontal => onHorizontal(),
        Orientation.Vertical => onVertical(),
        _ => onHorizontal()
    };  
}
