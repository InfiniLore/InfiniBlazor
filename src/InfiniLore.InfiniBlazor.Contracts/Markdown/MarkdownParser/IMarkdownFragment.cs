// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownFragment {
    MdElement Element { get; }
    IMdNode Node { get; }
    HandlerOrigin Origin { get; }
    Match? Match { get; }
    string? Content { get; }
    
    [MemberNotNull(nameof(Match))] bool IsMatch { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsMatch(Match match, IMdNode node, HandlerOrigin origin);
    void AsElement(string content, IMdNode node, HandlerOrigin origin, MdElement element);
}
