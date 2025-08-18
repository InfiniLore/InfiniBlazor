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
    string DefaultBodyClasses { get; }

    bool TryGetCalloutMakeup(string id, [NotNullWhen(true)] out string? cssClasses, [NotNullWhen(true)] out string? bodyClasses);
    bool TryGetLucideIcon(string id, [NotNullWhen(true)] out string? iconName);
}
