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
    public required FrozenDictionary<string, ICalloutStyle> CalloutStyles { private get; init; }
    public required FrozenDictionary<string, string> AliasMap {private get; init;}
    
    public string DefaultLucideIconName { get; init; } = string.Empty;
    public string DefaultCssClasses { get; init; } = string.Empty;
    public string DefaultBodyClasses { get; init; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetCalloutStyle(string id, [NotNullWhen(true)] out ICalloutStyle? style) {
        if (AliasMap.TryGetValue(id, out string? correctName)) id = correctName;
        return CalloutStyles.TryGetValue(id, out style);
    }
}

