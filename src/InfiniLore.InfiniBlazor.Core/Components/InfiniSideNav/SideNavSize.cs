// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum SideNavSize {
    Minimum = 14,
    Small = 24,
    Medium = 48,
    Large = 64
}

public static class SideNavMenuSizesExtensions{
    public static string ToTailwindWidth(this SideNavSize size) {
        return size switch {
            SideNavSize.Minimum => "w-14",
            SideNavSize.Small => "w-24",
            SideNavSize.Medium => "w-48",
            SideNavSize.Large => "w-64",
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }
}