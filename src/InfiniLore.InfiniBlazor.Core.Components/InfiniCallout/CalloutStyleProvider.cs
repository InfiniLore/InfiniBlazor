// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.Callouts;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CalloutStyleProvider : ICalloutStyleProvider {
    public required FrozenDictionary<string, ICalloutStyle> CalloutStyles { private get; init; }
    public required FrozenDictionary<string, string> AliasMap { private get; init; }

    public required ICalloutStyle DefaultStyle { get; init; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetCalloutStyle(string id, [NotNullWhen(true)] out ICalloutStyle? style) {
        if (AliasMap.TryGetValue(id, out string? correctName)) id = correctName;
        return CalloutStyles.TryGetValue(id, out style);
    }
}
