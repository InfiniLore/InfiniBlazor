// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeModifier {
    Dictionary<string, Range> Attributes { get; }
    string OriginalInput { get;}
    ReadOnlySpan<char> OriginalInputSpan { get; }
    bool TryGetAttributeValue(string key, [NotNullWhen(true)] out string? value);
    bool TryGetAttributeFlag(string key, out bool value);
}
