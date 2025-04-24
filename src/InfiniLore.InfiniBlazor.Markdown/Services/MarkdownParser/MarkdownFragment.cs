// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownFragment : IMarkdownFragment, IResettable {
    public MarkdownElement Element { get; private set; }
    public IMarkdownSyntaxNode Node { get; private set; } = null!;
    public HandlerOrigin Origin { get; private set; }
    public Match? Match { get; private set; }
    public string? Content { get; private set; }

    [MemberNotNull(nameof(Match))] public bool IsMatch { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void AsMatch(Match match, IMarkdownSyntaxNode node, HandlerOrigin origin) {
        Node = node;
        Origin = origin;
        Match = match;
        IsMatch = true;
    }

    public void AsElement(string? content, IMarkdownSyntaxNode node, HandlerOrigin origin, MarkdownElement element) {
        Node = node;
        Origin = origin;
        Content = content;
        Element = element;
        IsMatch = false;
    }
    
    public bool TryReset() {
        Node = null!;
        Origin = HandlerOrigin.NotSkipped;
        Match = null;
        Content = null;
        IsMatch = false;
        Element = MarkdownElement.Undefined;

        return true;
    }
}
