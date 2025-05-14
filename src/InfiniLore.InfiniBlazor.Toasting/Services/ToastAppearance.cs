// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastAppearance(
    string IconName,
    string TailwindData,
    bool CanBeDismissed
) : IToastAppearance {
    public static IToastAppearance Default = new ToastAppearance(
        "info",
        "bg-green-100 border-green-500 text-green-900 hover:bg-green-200",
        true
    );
}
