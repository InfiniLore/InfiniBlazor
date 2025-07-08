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
public sealed class MdSyntaxFragment : IResettable {
    private IMdSyntaxNode? _parentNode;
    private IMdSyntaxNode? _childNode;
    private Match? _match;
    private MdSyntaxHandlerOrigin _handlerOrigin;
    private bool _isMatch;

    public static ObjectPool<MdSyntaxFragment> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragment>(32);

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxFragment AsUnhandledMatch(Match match, IMdSyntaxNode node, MdSyntaxHandlerOrigin handlerOrigin) {
        MdSyntaxFragment fragment = Pool.Get();
        fragment._parentNode = node;
        fragment._match = match;
        fragment._handlerOrigin = handlerOrigin;
        fragment._isMatch = true;
        return fragment;
    }

    public static MdSyntaxFragment AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode) {
        MdSyntaxFragment fragment = Pool.Get();
        fragment._parentNode = parentNode;
        fragment._childNode = childNode;
        fragment._isMatch = false;
        return fragment;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetAsMatch([NotNullWhen(true)] out Match? match, [NotNullWhen(true)] out IMdSyntaxNode? parentNode, out MdSyntaxHandlerOrigin parentOrigin) {
        match = _match;
        parentNode = _parentNode;
        parentOrigin = _handlerOrigin;
        return _isMatch;
    }

    public bool TryGetAsProcessedNode([NotNullWhen(true)] out IMdSyntaxNode? parentNode, [NotNullWhen(true)] out IMdSyntaxNode? childNode) {
        parentNode = _parentNode;
        childNode = _childNode;
        return !_isMatch;
    }

    public bool TryReset() {
        return true;
    }

}
