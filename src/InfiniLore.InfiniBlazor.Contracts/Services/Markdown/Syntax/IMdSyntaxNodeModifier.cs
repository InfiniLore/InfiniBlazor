// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeModifier : IEquatable<IMdSyntaxNodeModifier> {
    FrozenDictionary<string, Range> Attributes { get; }
    string OriginalInput { get;}
    ReadOnlySpan<char> OriginalInputSpan { get; }
    bool TryGetValue(string key, [NotNullWhen(true)] out string? value);
    bool TryGetFlag(string key, out bool value);
    
    void ReturnToPool();
}
