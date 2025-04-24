// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode, IResettable {
    private const int MinimumCapacity = 4;
    private int _childCount;
    private MdNode[] _childNodes = ArrayPool<MdNode>.Shared.Rent(MinimumCapacity);
    private readonly HashSet<string> _classes = new();
    private readonly Dictionary<string, string> _attributes = new();

    public MdElement Element { get; private set; } = MdElement.Undefined;
    public string? Content { get; private set; }

    public IReadOnlyDictionary<string, string> Attributes => _attributes;
    public IReadOnlySet<string> Classes => _classes;

    public IMdNode Parent { get; private set; } = null!;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static MdNode AsRootNode() {
        MdNode node = PoolCache.MdNodePool.Get();
        node.Parent = node;
        return node;
    }

    public ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMdNode {
        length = _childCount;
        if (_childCount == 0) return ReadOnlySpan<T>.Empty;

        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<MdNode, T>(ref MemoryMarshal.GetArrayDataReference(_childNodes)),
            _childCount
        );
    }

    public IMdNode AddChildNode(MdElement element) => CreateChildNode(element);

    public IMdNode WithContent(string content) {
        if (_childNodes.LastOrDefault() is not { Element: MdElement.Content } lastNode) {
            CreateChildNode(MdElement.Content, content);
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

    public IMdNode WithHtmlContent(string content) {
        CreateChildNode(MdElement.HtmlContent, content);
        return this;
    }

    public IMdNode WithClass(string className) {
        _classes.Add(className);
        return this;
    }

    public IMdNode WithAttribute(string key, string value) {
        _attributes.AddOrUpdate(key, value);
        return this;
    }

    private MdNode CreateChildNode(MdElement element, string? content = null) {
        MdNode child = PoolCache.MdNodePool.Get();
        child.Element = element;
        child.Content = content;
        child.Parent = this;

        // Check if we need to resize
        if (_childCount == _childNodes.Length) {
            int newSize = Math.Max(MinimumCapacity, _childNodes.Length * 2);
            MdNode[] newArray = ArrayPool<MdNode>.Shared.Rent(newSize);
            Array.Copy(_childNodes, newArray, _childCount);

            ArrayPool<MdNode>.Shared.Return(_childNodes, true);
            _childNodes = newArray;
        }

        _childNodes[_childCount++] = child;

        return child;
    }

    public bool TryReset() {
        if (_childNodes.Length > 0) ArrayPool<MdNode>.Shared.Return(_childNodes, true);
        _childNodes = ArrayPool<MdNode>.Shared.Rent(MinimumCapacity);
        _childCount = 0;

        _classes.Clear();
        _attributes.Clear();
        Element = MdElement.Undefined;
        Content = null;
        Parent = null!;
        return true;
    }
}
