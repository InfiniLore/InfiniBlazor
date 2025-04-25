// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownSyntaxNode : IMarkdownSyntaxNode, IResettable {
    private const int MinimumCapacity = 2;
    private int _childCount;
    private MarkdownSyntaxNode[] _childNodes = ArrayPool<MarkdownSyntaxNode>.Shared.Rent(MinimumCapacity);

    private int _attributeCount;
    private MarkdownAttribute[] _attributes = ArrayPool<MarkdownAttribute>.Shared.Rent(MinimumCapacity);
    private readonly Dictionary<MarkdownAttribute, string> _attributesSource = new();

    public MarkdownElement Element { get; private set; } = MarkdownElement.Undefined;
    public string? Content { get; private set; }
    
    public IMarkdownSyntaxNode Parent { get; private set; } = null!;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMarkdownSyntaxNode {
        length = _childCount;
        if (_childCount == 0) return ReadOnlySpan<T>.Empty;
        
        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<MarkdownSyntaxNode, T>(ref MemoryMarshal.GetArrayDataReference(_childNodes)),
            _childCount
        );
    }
    public ReadOnlySpan<MarkdownAttribute> GetAttributes(out int count, out IReadOnlyDictionary<MarkdownAttribute, string> source) {
        source = _attributesSource;
        count = _attributeCount;
        if (_attributeCount == 0) return ReadOnlySpan<MarkdownAttribute>.Empty;
        return _attributes;
    }

    public IMarkdownSyntaxNode AddChildNode(MarkdownElement element, string? content = null) {
        MarkdownSyntaxNode child = PoolCache.MdNodePool.Get();
        child.Element = element;
        child.Content = content;
        child.Parent = this;

        // Check if we need to resize
        if (_childCount == _childNodes.Length) {
            int newSize = Math.Max(MinimumCapacity, _childNodes.Length * 2);
            MarkdownSyntaxNode[] newArray = ArrayPool<MarkdownSyntaxNode>.Shared.Rent(newSize);
            Array.Copy(_childNodes, newArray, _childCount);

            ArrayPool<MarkdownSyntaxNode>.Shared.Return(_childNodes, true);
            _childNodes = newArray;
        }

        _childNodes[_childCount++] = child;

        return child;
    }


    public IMarkdownSyntaxNode WithContent(string? content) {
        if (content.IsNullOrWhiteSpace()) return this;
        if (_childNodes.LastOrDefault() is not { Element: MarkdownElement.Content } lastNode) {
            AddChildNode(MarkdownElement.Content, content);
            return this;
        }

        int contentLength = lastNode.Content?.Length ?? 0;
        int length = contentLength + content.Length;
        lastNode.Content = string.Create(
            length,
            (contentLength, OriginalContent: lastNode.Content, NewContent: content),
            action: static (span, state) => {
                state.OriginalContent.AsSpan().CopyTo(span);
                state.NewContent.AsSpan().CopyTo(span[state.contentLength..]);
            });

        return this;
    }

    public IMarkdownSyntaxNode WithHtmlContent(string? content) {
        AddChildNode(MarkdownElement.HtmlContent, content);
        return this;
    }

    public IMarkdownSyntaxNode WithAttribute(MarkdownAttribute attribute, string value) {
        // Check if we need to resize
        if (_attributeCount == _attributes.Length) {
            int newSize = Math.Max(MinimumCapacity, _attributes.Length * 2);
            MarkdownAttribute[] newArray = ArrayPool<MarkdownAttribute>.Shared.Rent(newSize);
            Array.Copy(_attributes, newArray, _childCount);

            ArrayPool<MarkdownAttribute>.Shared.Return(_attributes, true);
            _attributes = newArray;
        }
        _attributes[_attributeCount++] = attribute;
        _attributesSource.AddOrUpdate(attribute, value);
        return this;
    }

    public bool TryReset() {
        if (_childNodes.Length > 0) {
            ArrayPool<MarkdownSyntaxNode>.Shared.Return(_childNodes, true);
            _childNodes = ArrayPool<MarkdownSyntaxNode>.Shared.Rent(MinimumCapacity);
        }
        _childCount = 0;

        _attributeCount = 0;
        if (_attributes.Length > 0) {
            ArrayPool<MarkdownAttribute>.Shared.Return(_attributes, true);
            _attributes = ArrayPool<MarkdownAttribute>.Shared.Rent(MinimumCapacity);
        }
        _attributesSource.Clear();
        Element = MarkdownElement.Undefined;
        Content = null;
        Parent = null!;
        return true;
    }
}
