// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Flags]
public enum NavLayout {
    TopPrimary    = 1 << 0,
    BottomPrimary = 1 << 1,
    LeftPrimary   = 1 << 2,
    RightPrimary  = 1 << 3,
    
    SidesPrimary = LeftPrimary | RightPrimary,
    BarsPrimary = TopPrimary | BottomPrimary,
}

public static class NavLayoutExtensions {
    public static bool HasFastFlag(this NavLayout layout, NavLayout flag) => (layout & flag) == flag;
}
