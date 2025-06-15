// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum ToastAppearance {
    Default,
    Info,
    Success,
    Warning,
    Error,
    Debug,
    Achievement
}

public static class ToastAppearanceExtensions {
    public static string ToName(this ToastAppearance appearance) => appearance switch {
        ToastAppearance.Default => "default",
        ToastAppearance.Info => "info",
        ToastAppearance.Success => "success",
        ToastAppearance.Warning => "warning",
        ToastAppearance.Error => "error",
        ToastAppearance.Debug => "debug",
        ToastAppearance.Achievement => "achievement",
        _ => "default"
    };
}