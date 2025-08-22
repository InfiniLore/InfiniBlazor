// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxFragment : IResettable {
    private IMdSyntaxNode? _parentNode;
    private IMdSyntaxNode? _childNode;
    private Match? _match;
    private MarkdownStringMdSyntaxSerializerOrigin _handlerOrigin;

    public static ObjectPool<MdSyntaxFragment> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragment>(32);

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxFragment AsUnhandledMatch(Match match, IMdSyntaxNode node, MarkdownStringMdSyntaxSerializerOrigin handlerOrigin) {
        MdSyntaxFragment fragment = Pool.Get();
        fragment._parentNode = node;
        fragment._match = match;
        fragment._handlerOrigin = handlerOrigin;
        return fragment;
    }

    public static MdSyntaxFragment AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode) {
        MdSyntaxFragment fragment = Pool.Get();
        fragment._parentNode = parentNode;
        fragment._childNode = childNode;
        return fragment;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetAsMatch(
        [NotNullWhen(true)] out Match? match,
        [NotNullWhen(true)] out IMdSyntaxNode? parentNode,
        out MarkdownStringMdSyntaxSerializerOrigin parentOrigin
    ) {
        match = _match;
        parentNode = _parentNode;
        parentOrigin = _handlerOrigin;
        return _match is not null;
    }

    public bool TryGetAsProcessedNode(
        [NotNullWhen(true)] out IMdSyntaxNode? parentNode,
        [NotNullWhen(true)] out IMdSyntaxNode? childNode
    ) {
        parentNode = _parentNode;
        childNode = _childNode;
        return _match is null;
    }

    public bool TryReset() {
        _parentNode = null;
        _childNode = null;
        _match = null;
        _handlerOrigin = default;
        return true;
    }

}
