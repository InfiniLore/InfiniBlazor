// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfoToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-blue) [&_svg]:text-(--color-blue-light) [&_svg:hover]:text-(--color-accent)";
    protected override string BodyClasses => "infini-bg-(--color-blue-dark) border-(--color-blue) text-(--color-base-10)";
    protected override string IconName => LucideNames.Info;
}
