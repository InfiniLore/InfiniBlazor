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
    string? RawInput { get; }
    [MemberNotNull(nameof(Match))] bool IsMatch { get; }
    [MemberNotNull(nameof(RawInput))] bool IsRawInput { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsMatch(IMdNode node, ParserOrigin origin, Match match);
    void AsString(IMdNode node, ParserOrigin origin, string value);
}
