// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class SuccessToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-green-light [&_svg]:text-infini-green-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-green-dark border-infini-green text-infini-base-10";
    protected override string IconName => LucideNames.CircleCheckBig;
}
