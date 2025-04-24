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
    MarkdownElement Element { get; }
    IMarkdownSyntaxNode Node { get; }
    HandlerOrigin Origin { get; }
    Match? Match { get; }
    string? Content { get; }
    
    [MemberNotNull(nameof(Match))] bool IsMatch { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsMatch(Match match, IMarkdownSyntaxNode node, HandlerOrigin origin);
    void AsElement(string content, IMarkdownSyntaxNode node, HandlerOrigin origin, MarkdownElement element);
}
