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
    public required FrozenDictionary<string, (string LucideIcon, string CssClasses)> CalloutMakeup { private get; init; }
    public string DefaultLucideIconName { get; init; } = string.Empty;
    public string DefaultCssClasses { get; init; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetCalloutMakeup(string id, [NotNullWhen(true)] out string? cssClasses) {
        if (!CalloutMakeup.TryGetValue(id, out (string LucideIcon, string CssClasses) box)) {
            cssClasses = null;
            return false;
        }

        cssClasses = box.CssClasses;
        return cssClasses.IsNotNullOrWhiteSpace();
    }

    public bool TryGetLucideIcon(string id, [NotNullWhen(true)] out string? iconName) {
        if (!CalloutMakeup.TryGetValue(id, out (string LucideIcon, string CssClasses) box)) {
            iconName = null;
            return false;
        }

        iconName = box.LucideIcon;
        return iconName.IsNotNullOrWhiteSpace();
    }
}

