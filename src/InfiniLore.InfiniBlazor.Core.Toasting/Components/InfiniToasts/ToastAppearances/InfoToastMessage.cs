// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class InfoToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-blue [&_svg]:text-infini-blue-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-blue-dark border-infini-blue text-infini-base-10";
    protected override string IconName => LucideNames.Info;
}
