// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode {
    private readonly List<MdNode> _childNodes = new();
    private readonly HashSet<string> _classes = new();
    private readonly Dictionary<string, string> _attributes = new();

    public MdElement Element { get; private init; } = MdElement.Undefined;
    public string? Content { get; private set; }

    public IReadOnlyCollection<IMdNode> Children => _childNodes;
    public IReadOnlyDictionary<string, string> Attributes => _attributes;
    public IReadOnlyCollection<string> Classes => _classes;

    public IMdNode Parent { get; private set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
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
        var child = new MdNode {
            Element = element,
            Content = content
        };

        _childNodes.Add(child);
        child.Parent = this;
        return child;
    }
}
