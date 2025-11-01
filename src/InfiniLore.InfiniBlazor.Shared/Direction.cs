// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Direction {
    LeftToRight = 0,
    RightToLeft = 1
}

public static class DirectionExtensions {
    public static string ToFlexRow(this Direction direction) => direction switch {
        Direction.LeftToRight => "flex-row",
        Direction.RightToLeft => "flex-row-reverse",
        _ => "flex-row"
    };
    public static string ToFlexCol(this Direction direction) => direction switch {
        Direction.LeftToRight => "flex-col",
        Direction.RightToLeft => "flex-col-reverse",
        _ => "flex-col"
    };
}