// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum Size {
    Xxs,
    Xs,
    S,
    M,
    L,
    Xl,
    Xxl
}


public static class SizeUtilities {
    public static int ToLucideIconSize(this Size size) => size switch {
        Size.Xxs => 6,
        Size.Xs => 10,
        Size.S => 16,
        Size.M => 24,
        Size.L => 36,
        Size.Xl => 48,
        Size.Xxl => 60,
        _ => 24
    };

    public static string ToTailwindRounded(this Size size) => size switch {
        Size.Xxs => "rounded-xs",
        Size.Xs => "rounded-sm",
        Size.S => "rounded",
        Size.M => "rounded-md",
        Size.L => "rounded-lg",
        Size.Xl => "rounded-xl",
        Size.Xxl => "rounded-2xl",
        _ => "rounded"
    };
}