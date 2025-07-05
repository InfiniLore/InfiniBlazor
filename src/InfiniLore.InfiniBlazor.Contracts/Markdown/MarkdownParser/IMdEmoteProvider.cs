// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdEmoteProvider{
    bool TryGetValue(string lookupValue,[NotNullWhen(true)] out string? s);
    bool TryGetValueSpan(string lookupValue, out ReadOnlySpan<char> s);
    
    bool TryGetValue(in ReadOnlySpan<char> lookupValue, [NotNullWhen(true)] out string? s);
    bool TryGetValueSpan(in ReadOnlySpan<char> lookupValue, out ReadOnlySpan<char> s);
}
