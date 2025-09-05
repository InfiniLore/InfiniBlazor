// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class SuccessToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-green-light) [&_svg]:text-(--color-green-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-green-dark) border-(--color-green) text-(--color-base-10)";
    protected override string IconName => LucideNames.CircleCheckBig;
}
