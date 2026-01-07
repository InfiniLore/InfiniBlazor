// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeModifier : IEquatable<IMdSyntaxNodeModifier> {
    FrozenDictionary<string, Range> Attributes { get; }
    string OriginalInput { get; }
    ReadOnlySpan<char> OriginalInputSpan { get; }
    bool TryGetValue(string key, [NotNullWhen(true)] out string? value);
    bool TryGetFlag(string key, out bool value);

    void ReturnToPool();

    bool TryGetIconName([NotNullWhen(true)] out string? iconName);
    bool TryGetTitle([NotNullWhen(true)] out string? title);
    bool TryGetSize(out (int Width, int Height) size);
    bool TryGetFit(out bool state);
    bool TryGetAlign([NotNullWhen(true)] out string? align);
    bool TryGetColor([NotNullWhen(true)] out string? color);
    bool TryGetStyle([NotNullWhen(true)] out string? style);
}
