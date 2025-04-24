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
    public MdElement Element { get; private set; }
    public IMdNode Node { get; private set; } = null!;
    public HandlerOrigin Origin { get; private set; }
    public Match? Match { get; private set; }
    public string? Content { get; private set; }

    [MemberNotNull(nameof(Match))] public bool IsMatch { get; private set; }

    public void AsMatch(Match match, IMdNode node, HandlerOrigin origin) {
        Node = node;
        Origin = origin;
        Match = match;
        IsMatch = true;
    }

    public void AsElement(string? content, IMdNode node, HandlerOrigin origin, MdElement element) {
        Node = node;
        Origin = origin;
        Content = content;
        Element = element;
        IsMatch = false;
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryReset() {
        Node = null!;
        Origin = HandlerOrigin.NotSkipped;
        Match = null;
        Content = null;
        IsMatch = false;
        Element = MdElement.Undefined;

        return true;
    }

    public bool TryGetAsMatch([NotNullWhen(true)] out Match? match) {
        match = Match;
        return IsMatch;
    }
    
    public bool TryGetAsElement(out MdElement element, out string? content) {
        content = Content;
        element = Element;
        return !IsMatch;
    }
}
