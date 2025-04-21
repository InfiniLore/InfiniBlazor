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
    public string? Content { get; private set; } 
    public MdElement Element { get; private set; }
    
    [MemberNotNull(nameof(Match))] public bool IsMatch { get; private set; }
    [MemberNotNull(nameof(Content))] public bool IsContent { get; private set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Clear() {
        Node = null!;
        Origin = ParserOrigin.NotSkipped;
        Match = null;
        Content = null;
        IsMatch = false;
        IsContent = false;
        Element = MdElement.Undefined;
    }
    
    public void AsMatch(IMdNode node, ParserOrigin origin, Match match) {
        Node = node;
        Origin = origin;
        Match = match;
        IsMatch = true;
    }
    
    public void AsContent(IMdNode node, ParserOrigin origin, string value, MdElement element = MdElement.Undefined) {
        Node = node;
        Origin = origin;
        Content = value;
        IsContent = true;
        Element = element;
    }
}
