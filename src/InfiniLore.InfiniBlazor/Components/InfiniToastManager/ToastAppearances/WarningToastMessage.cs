// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WarningToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-orange-light) ] [&_svg]:text-(--color-orange-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-orange-dark) border-(--color-orange) text-(--color-base-10)";
    protected override string IconName => LucideNames.TriangleAlert;
}
