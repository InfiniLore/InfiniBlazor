// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ErrorToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-red-light)";
    protected override string BodyClasses => "infini-bg-(--color-red-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.Ban;
}
