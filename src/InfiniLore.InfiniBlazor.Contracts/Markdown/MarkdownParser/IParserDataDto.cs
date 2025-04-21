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
    string? NewContent { get; }
    [MemberNotNull(nameof(Match))] bool IsMatch { get; }
    [MemberNotNull(nameof(NewContent))] bool IsNewContent { get; }
    bool IsOriginalContent { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsMatch(IMdNode node, ParserOrigin origin, Match match);
    void AsNewContent(IMdNode node, ParserOrigin origin, string value);
    void AsOriginalContent(IMdNode node, ParserOrigin origin, Range range);
}
