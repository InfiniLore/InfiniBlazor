// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ErrorToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-red-light [&_svg]:text-infini-red-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-red-dark border-infini-red text-infini-base-10";
    protected override string IconName => LucideNames.Ban;
}
