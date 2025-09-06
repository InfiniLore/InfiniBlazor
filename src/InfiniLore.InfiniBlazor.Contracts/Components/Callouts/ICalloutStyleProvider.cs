// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICalloutStyleProvider {
    ICalloutStyle DefaultStyle { get; }

    bool TryGetCalloutStyle(string id, [NotNullWhen(true)] out ICalloutStyle? style);
}
