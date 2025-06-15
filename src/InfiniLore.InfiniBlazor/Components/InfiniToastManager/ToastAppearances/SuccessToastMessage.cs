// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SuccessToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "infini-bg-(--color-green-dark) border-2 border-(--color-green)";
    protected override string BodyClasses => "infini-bg-(--color-green-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.Check;
}
