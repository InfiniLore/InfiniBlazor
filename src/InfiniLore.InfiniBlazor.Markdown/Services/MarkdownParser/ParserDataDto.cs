// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ParserDataDto : IParserDataDto {
    public IMdNode Node { get; private set; } = null!;
    public ParserOrigin Origin { get; private set; }
    public Match? Match { get; private set; }
    public string? NewContent { get; private set; }
    public Range OriginalContent { get; private set; }
    
    [MemberNotNull(nameof(Match))] public bool IsMatch { get; private set; }
    [MemberNotNull(nameof(NewContent))] public bool IsNewContent { get; private set; }
    public bool IsOriginalContent { get; private set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Clear() {
        Node = null!;
        Origin = ParserOrigin.NotSkipped;
        Match = null;
        NewContent = null;
        IsMatch = false;
        IsNewContent = false;
    }
    
    public void AsMatch(IMdNode node, ParserOrigin origin, Match match) {
        Node = node;
        Origin = origin;
        Match = match;
        IsMatch = true;
    }
    
    public void AsNewContent(IMdNode node, ParserOrigin origin, string value) {
        Node = node;
        Origin = origin;
        NewContent = value;
        IsNewContent = true;
    }
    
    public void AsOriginalContent(IMdNode node, ParserOrigin origin, Range range) {
        Node = node;
        Origin = origin;
        OriginalContent = range;
        IsOriginalContent = true;
    }
}
