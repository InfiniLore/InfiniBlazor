// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IParserDataDto {
    IMdNode Node { get; }
    ParserOrigin Origin { get; }
    Match? Match { get; }
    string? Content { get; }
    [MemberNotNull(nameof(Match))] bool IsMatch { get; }
    [MemberNotNull(nameof(Content))] bool IsElement { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsMatch(Match match, IMdNode node, ParserOrigin origin);
    void AsElement(string content, IMdNode node, ParserOrigin origin, MdElement element);
}
