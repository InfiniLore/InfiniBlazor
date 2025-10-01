// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Toasting.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class AchievementToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-infini-yellow-light [&_svg]:text-infini-yellow-light [&_svg:hover]:text-infini-accent";
    protected override string BodyClasses => "overload-bg-infini-yellow-dark border-infini-yellow text-infini-base-10";
    protected override string IconName => LucideNames.Star;
}
