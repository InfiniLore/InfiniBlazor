// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Direction {
    LeftToRight = 0,
    Ltr = LeftToRight,
    
    RightToLeft = 1,
    Rtl = RightToLeft,
}


public static class DirectionExtensions {
    public static string ToFlexRow(this Direction direction) => direction is Direction.LeftToRight ? "flex-row" : "flex-row-reverse";
    public static string ToFlexCol(this Direction direction) => direction is Direction.LeftToRight ? "flex-col" : "flex-col-reverse";
}