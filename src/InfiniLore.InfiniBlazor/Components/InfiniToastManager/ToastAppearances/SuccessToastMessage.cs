// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SuccessToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-green-light)";
    protected override string BodyClasses => "infini-bg-(--color-green-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.Check;
}
