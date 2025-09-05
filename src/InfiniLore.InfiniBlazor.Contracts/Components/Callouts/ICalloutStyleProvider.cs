// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components.Callouts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICalloutStyleProvider {
    ICalloutStyle DefaultStyle { get; }

    bool TryGetCalloutStyle(string id, [NotNullWhen(true)] out ICalloutStyle? style);
}
