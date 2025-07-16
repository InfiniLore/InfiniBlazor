// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum NavLayoutLocation {
    Top,
    Left,
    Right,
    Bottom
}

public static class NavLayoutLocationExtensions {
    public static bool IsHorizontal(this NavLayoutLocation location) => location is NavLayoutLocation.Bottom or NavLayoutLocation.Top;
    public static bool IsVertical(this NavLayoutLocation location) => location is NavLayoutLocation.Left or NavLayoutLocation.Right;

    public static string OnOrientation(this NavLayoutLocation location, Func<string> onHorizontal, Func<string> onVertical) 
        => location.IsHorizontal() 
            ? onHorizontal()
            : onVertical();

    public static string ToFlex(this NavLayoutLocation location) => location switch {
        NavLayoutLocation.Top => "flex-row",
        NavLayoutLocation.Left => "flex-col",
        NavLayoutLocation.Right => "flex-col",
        NavLayoutLocation.Bottom => "flex-row",
        _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
    };
    
    public static string ToFull(this NavLayoutLocation location) => location switch {
        NavLayoutLocation.Top => "w-full",
        NavLayoutLocation.Left => "h-full",
        NavLayoutLocation.Right => "h-full",
        NavLayoutLocation.Bottom => "w-full",
        _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
    };
}