// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfoToastMessage : ToastMessageBase {
    protected override string HeaderClasses => "text-(--color-blue)";
    protected override string BodyClasses => "infini-bg-(--color-blue-dark) border-none text-(--text)";
    protected override string IconName => LucideNames.Info;
}
