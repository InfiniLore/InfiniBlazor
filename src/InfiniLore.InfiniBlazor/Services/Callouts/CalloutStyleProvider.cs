// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Callouts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CalloutStyleProvider : ICalloutStyleProvider {
    public required FrozenDictionary<string, (string LucideIcon, string CssClasses, string bodyClasses)> CalloutMakeup { private get; init; }
    public string DefaultLucideIconName { get; init; } = string.Empty;
    public string DefaultCssClasses { get; init; } = string.Empty;
    public string DefaultBodyClasses { get; init; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetCalloutMakeup(string id, [NotNullWhen(true)] out string? cssClasses,[NotNullWhen(true)] out string? bodyClasses) {
        if (!CalloutMakeup.TryGetValue(id, out (string LucideIcon, string CssClasses, string bodyClasses) box)) {
            cssClasses = null;
            bodyClasses = null;
            return false;
        }

        cssClasses = box.CssClasses;
        bodyClasses = box.bodyClasses;
        return cssClasses.IsNotNullOrWhiteSpace();
    }

    public bool TryGetLucideIcon(string id, [NotNullWhen(true)] out string? iconName) {
        if (!CalloutMakeup.TryGetValue(id, out (string LucideIcon, string CssClasses, string bodyClasses) box)) {
            iconName = null;
            return false;
        }

        iconName = box.LucideIcon;
        return iconName.IsNotNullOrWhiteSpace();
    }
}

