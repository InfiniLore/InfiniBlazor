// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HtmlSymbolLookup {
    private static readonly FrozenDictionary<string, string> Lookup = new Dictionary<string, string>(6) {
        { "&copy;", "\u00a9" },
        { "<br/>", "<br/>" },
        { "&", "&amp;" },
        { "<", "&lt;" },
        { ">", "&gt;" },
        { "\"", "&quot;" },
    }.ToFrozenDictionary();
    
    public static FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup { get; } = Lookup.GetAlternateLookup<ReadOnlySpan<char>>();
    public static Regex Regex { get; } = new(string.Join('|', Lookup.Keys), RegexOptions.Compiled | RegexOptions.Singleline);
}
