// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WarningToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-orange-light ] [&_svg]:text-infini-orange-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-orange-dark border-infini-orange text-infini-base-10";
    protected override string IconName => LucideNames.TriangleAlert;
}
