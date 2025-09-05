// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class AchievementToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-yellow-light) [&_svg]:text-(--color-yellow-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-yellow-dark) border-(--color-yellow) text-(--color-base-10)";
    protected override string IconName => LucideNames.Star;
}
