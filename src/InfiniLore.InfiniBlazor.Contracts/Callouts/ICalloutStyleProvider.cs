// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Callouts;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICalloutStyleProvider {
    string DefaultLucideIconName { get; }
    string DefaultCssClasses { get; }

    bool TryGetCalloutMakeup(string id, [NotNullWhen(true)] out string? cssClasses);
    bool TryGetLucideIcon(string id, [NotNullWhen(true)] out string? iconName);
}
