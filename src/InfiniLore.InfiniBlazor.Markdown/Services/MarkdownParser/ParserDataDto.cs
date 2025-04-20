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
    public IMdNode Node { get; private set; }
    public ParserOrigin Origin { get; private set; }
    public Match? Match { get; private set; }
    public string? RawInput { get; private set; }
    
    [MemberNotNull(nameof(Match))] public bool IsMatch { get; private set; }
    [MemberNotNull(nameof(RawInput))] public bool IsRawInput { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Clear() {
        Node = null!;
        Origin = ParserOrigin.NotSkipped;
        Match = null;
        RawInput = null;
        IsMatch = false;
        IsRawInput = false;
    }
    
    public void AsMatch(IMdNode node, ParserOrigin origin, Match match) {
        Node = node;
        Origin = origin;
        Match = match;
        IsMatch = true;
    }
    
    public void AsString(IMdNode node, ParserOrigin origin, string value) {
        Node = node;
        Origin = origin;
        RawInput = value;
        IsRawInput = true;
    }
}
