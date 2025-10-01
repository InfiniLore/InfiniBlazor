// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class DebugToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-purple-light [&_svg]:text-infini-purple-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-purple-dark border-infini-purple text-infini-base-10";
    protected override string IconName => LucideNames.Bug;
}
