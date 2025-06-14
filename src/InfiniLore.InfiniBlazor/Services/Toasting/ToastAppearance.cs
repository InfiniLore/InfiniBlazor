// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastAppearance(
    string? IconName,
    string TailwindData,
    bool CanBeDismissed
) : IToastAppearance {
    public static readonly IToastAppearance Default = new ToastAppearance(
        "info",
        "bg-green-950 border-green-600 text-green-500 hover:bg-green-900",
        true
    );
}
