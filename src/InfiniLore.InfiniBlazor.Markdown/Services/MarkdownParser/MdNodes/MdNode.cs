// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode, IResettable {
    private readonly List<MdNode> _childNodes = new();
    private readonly HashSet<string> _classes = new();
    private readonly Dictionary<string, string> _attributes = new();

    public MdElement Element { get; private set; } = MdElement.Undefined;
    public string? Content { get; private set; }

    public IReadOnlyList<IMdNode> Children => _childNodes;
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
    
    public IMdNode AddChildNode(MdElement element) => CreateChildNode(element);

    public IMdNode WithContent(string content) {
        if (_childNodes.LastOrDefault() is not { Element: MdElement.Content } lastNode) CreateChildNode(MdElement.Content, content);
        else lastNode.Content = string.Concat(lastNode.Content, content.AsSpan());

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

        _childNodes.EnsureCapacity(_childNodes.Count + 1);
        _childNodes.Add(child);
        return child;
    }

    public bool TryReset() {
        _childNodes.Clear();
        _classes.Clear();
        _attributes.Clear();
        Element = MdElement.Undefined;
        Content = null;
        Parent = null!;
        return true;
    }
}
