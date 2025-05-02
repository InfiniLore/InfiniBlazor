// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum SideNavMenuSize {
    Minimum = 14,
    Small = 24,
    Medium = 48,
    Large = 64
}

public static class SideNavMenuSizesExtensions{
    public static string ToTailwindWidth(this SideNavMenuSize size) {
        return size switch {
            SideNavMenuSize.Minimum => "w-14",
            SideNavMenuSize.Small => "w-24",
            SideNavMenuSize.Medium => "w-48",
            SideNavMenuSize.Large => "w-64",
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }
}