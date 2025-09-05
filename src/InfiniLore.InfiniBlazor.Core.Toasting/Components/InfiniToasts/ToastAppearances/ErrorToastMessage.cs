// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ErrorToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-red-light) [&_svg]:text-(--color-red-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-red-dark) border-(--color-red) text-(--color-base-10)";
    protected override string IconName => LucideNames.Ban;
}
