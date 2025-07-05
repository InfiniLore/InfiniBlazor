// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct MdSyntaxFragment {
    private IMdSyntaxNode? ParentNode { get; init; }
    private IMdSyntaxNode? ChildNode { get; init; }
    private MdSyntaxHandlerOrigin HandlerOrigin { get; init; }
    private Match? Match { get; init; }
    private SyntaxFragmentState State { get; init; }

    private enum SyntaxFragmentState {
        Match,
        ProcessedNode
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxFragment AsUnhandledMatch(Match match, IMdSyntaxNode node, MdSyntaxHandlerOrigin handlerOrigin)
        => new() {
            ParentNode = node,
            HandlerOrigin = handlerOrigin,
            Match = match,
            State = SyntaxFragmentState.Match
        };

    public static MdSyntaxFragment AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => new() {
            ParentNode = parentNode,
            ChildNode = childNode,
            State = SyntaxFragmentState.ProcessedNode,
        };

    public bool TryGetAsMatch([NotNullWhen(true)] out Match? match, [NotNullWhen(true)] out IMdSyntaxNode? parentNode, out MdSyntaxHandlerOrigin parentOrigin) {
        match = Match;
        parentNode = ParentNode;
        parentOrigin = HandlerOrigin;
        return State is SyntaxFragmentState.Match;
    }

    public bool TryGetAsProcessedNode([NotNullWhen(true)] out IMdSyntaxNode? parentNode, [NotNullWhen(true)] out IMdSyntaxNode? childNode) {
        parentNode = ParentNode;
        childNode = ChildNode;
        return State is SyntaxFragmentState.ProcessedNode;
    }
}
