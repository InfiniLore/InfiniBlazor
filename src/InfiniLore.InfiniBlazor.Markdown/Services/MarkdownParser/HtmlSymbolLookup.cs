// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services;

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
    
    public static FrozenDictionary<string, string> CodeBlockLookup { get; } = new Dictionary<string,string> {
        { "\r\n", "\n" },
        { "&", "&amp;" },
        { "<", "&lt;" },
        { ">", "&gt;" },
    }.ToFrozenDictionary();
    
    public static FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup { get; } = Lookup.GetAlternateLookup<ReadOnlySpan<char>>();
    public static FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> CodeBlockAlternateLookup { get; } = CodeBlockLookup.GetAlternateLookup<ReadOnlySpan<char>>();
    
    public static Regex Regex { get; } = new(string.Join('|', Lookup.Keys), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.Compiled);
    public static Regex CodeBlockRegex { get; } = new(string.Join('|', CodeBlockLookup.Keys), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.Compiled);
}
