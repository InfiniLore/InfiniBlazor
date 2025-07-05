// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxFragment : IResettable {
    private IMdSyntaxNode? ParentNode { get; set; }
    private IMdSyntaxNode? ChildNode { get; set; }
    private MdSyntaxHandlerOrigin HandlerOrigin { get; set; }
    private Match? Match { get; set; }
    private SyntaxFragmentState State { get; set; }

    private enum SyntaxFragmentState {
        Undefined,
        Match,
        ProcessedNode
    }
    
    public static ObjectPool<MdSyntaxFragment> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragment>(PoolingHelpers.ParsersRetained * PoolingHelpers.VisitorPerParserRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void AsUnhandledMatch(Match match, IMdSyntaxNode node, MdSyntaxHandlerOrigin origin) {
        ParentNode = node;
        HandlerOrigin = origin;
        Match = match;
        State = SyntaxFragmentState.Match;
    }

    public void AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode) {
        ParentNode = parentNode;
        ChildNode = childNode;
        State = SyntaxFragmentState.ProcessedNode;
    }

    public bool TryGetAsMatch([NotNullWhen(true)] out Match? match, [NotNullWhen(true)] out IMdSyntaxNode? parentNode, out MdSyntaxHandlerOrigin origin) {
        match = Match;
        parentNode = ParentNode;
        origin = HandlerOrigin;
        return State is SyntaxFragmentState.Match;
    }
    
    public bool TryGetAsProcessedNode([NotNullWhen(true)] out IMdSyntaxNode? parentNode, [NotNullWhen(true)] out IMdSyntaxNode? childNode) {
        parentNode = ParentNode;
        childNode = ChildNode;
        return State is SyntaxFragmentState.ProcessedNode;
    }
    
    public bool TryReset() {
        ParentNode = null;
        ChildNode = null;
        HandlerOrigin = MdSyntaxHandlerOrigin.Undefined;
        Match = null;
        State = SyntaxFragmentState.Undefined;

        return true;
    }
}
