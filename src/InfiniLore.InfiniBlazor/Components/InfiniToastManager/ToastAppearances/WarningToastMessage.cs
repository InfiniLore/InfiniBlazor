// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class WarningToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-orange-light)";
    protected override string BodyClasses => "infini-bg-(--color-orange-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.TriangleAlert;
}
