// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Alignment : byte {
    Left,
    Center,
    Right,
    Justify,
    Start,
    End
}

public static class AlignmentUtilities {
    public static string ToTextAlign(this Alignment alignment) => alignment switch {
        Alignment.Left => "text-left",
        Alignment.Center => "text-center",
        Alignment.Right => "text-right",
        Alignment.Justify => "text-justify",
        Alignment.Start => "text-start",
        Alignment.End => "text-end",
        _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
    };
}
