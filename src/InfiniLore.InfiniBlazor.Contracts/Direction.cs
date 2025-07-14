// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Direction {
    LeftToRight,
    RightToLeft
}


public static class DirectionExtensions {
    public static string ToFlexRow(this Direction direction) => direction is Direction.LeftToRight ? "flex-row" : "flex-row-reverse";
}