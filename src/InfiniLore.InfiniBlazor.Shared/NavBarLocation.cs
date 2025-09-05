// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum NavBarLocation : byte {
    Top,
    Left,
    Right,
    Bottom
}

public static class NavLayoutLocationExtensions {
    public static bool IsHorizontal(this NavBarLocation location) => location is NavBarLocation.Bottom or NavBarLocation.Top;
    public static bool IsVertical(this NavBarLocation location) => location is NavBarLocation.Left or NavBarLocation.Right;

    public static string OnOrientation(this NavBarLocation location, Func<string> onHorizontal, Func<string> onVertical) 
        => location.IsHorizontal() 
            ? onHorizontal()
            : onVertical();
}