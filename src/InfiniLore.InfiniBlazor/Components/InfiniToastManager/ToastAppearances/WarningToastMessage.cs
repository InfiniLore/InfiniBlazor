// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class WarningToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "infini-bg-(--color-orange-dark) border-2 border-(--color-orange) rounded-md";
    protected override string BodyClasses => "infini-bg-(--color-orange-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.TriangleAlert;
}
