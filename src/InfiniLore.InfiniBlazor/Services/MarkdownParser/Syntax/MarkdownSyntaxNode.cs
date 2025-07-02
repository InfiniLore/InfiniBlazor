// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace InfiniLore.InfiniBlazor.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownSyntaxNode : IMarkdownSyntaxNode, IResettable {
    private const int ChildrenMinimumCapacity = 2;
    private int _childCount;
    private MarkdownSyntaxNode[] _childNodes = ArrayPool<MarkdownSyntaxNode>.Shared.Rent(ChildrenMinimumCapacity);

    private const int AttributesMinimumCapacity = 1;
    private int _attributeCount;
    private MarkdownAttribute[]? _attributes;
    private string[]? _attributesSource;

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
    
    public bool TryGetAttributesSpan(out int count, out ReadOnlySpan<MarkdownAttribute> attributes, out ReadOnlySpan<string> sources) {
        if (_attributeCount == 0) {
            count = -1;
            attributes = ReadOnlySpan<MarkdownAttribute>.Empty;
            sources = ReadOnlySpan<string>.Empty;
            return false;
        }

        count = _attributeCount;
        attributes = _attributes;
        sources = _attributesSource;
        return true;
    }

    public IMarkdownSyntaxNode AddChildNode(MarkdownElement element, string? content = null) {
        MarkdownSyntaxNode child = MarkdownPoolCache.MarkdownSyntaxNodePool.Get();
        child.Element = element;
        child.Content = content;
        child.Parent = this;

        // Check if we need to resize
        if (_childCount == _childNodes.Length) {
            int newSize = Math.Max(ChildrenMinimumCapacity, _childNodes.Length * 2);
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
        if (_attributeCount == 0) {
            _attributes = ArrayPool<MarkdownAttribute>.Shared.Rent(AttributesMinimumCapacity);
            _attributesSource = ArrayPool<string>.Shared.Rent(AttributesMinimumCapacity);
        }
        else if (_attributeCount == _attributes!.Length) {
            int newSize =  _attributes!.Length * 2;
            MarkdownAttribute[] newArray = ArrayPool<MarkdownAttribute>.Shared.Rent(newSize);
            Array.Copy(_attributes, newArray, _attributeCount);
            ArrayPool<MarkdownAttribute>.Shared.Return(_attributes, true);
            _attributes = newArray;
            
            string[] newArraySource = ArrayPool<string>.Shared.Rent(newSize);
            Array.Copy(_attributesSource!, newArraySource, _attributeCount);
            ArrayPool<string>.Shared.Return(_attributesSource!, true);
            _attributesSource = newArraySource;
        }
        
        _attributes[_attributeCount] = attribute;
        _attributesSource![_attributeCount] = value;
        _attributeCount++;
        return this;
    }

    public bool TryReset() {
        if (_childNodes.Length > 0) {
            ArrayPool<MarkdownSyntaxNode>.Shared.Return(_childNodes, true);
            _childNodes = ArrayPool<MarkdownSyntaxNode>.Shared.Rent(ChildrenMinimumCapacity);
        }
        _childCount = 0;

        if (_attributes?.Length > 0) {
            ArrayPool<MarkdownAttribute>.Shared.Return(_attributes, true);
            ArrayPool<string>.Shared.Return(_attributesSource!, true);
            _attributes = null;
            _attributesSource = null;
        }
        _attributeCount = 0;

        Element = MarkdownElement.Undefined;
        Content = null;
        Parent = null!;
        return true;
    }
}
