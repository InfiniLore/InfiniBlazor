// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DebugToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-purple-light) ] [&_svg]:text-(--color-purple-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-purple-dark) border-none text-(--color-base-10)";
    protected override string IconName => LucideNames.Bug;
}
