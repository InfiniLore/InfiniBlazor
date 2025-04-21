// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode {
    private List<MdNode> ChildNodes { get; } = new();
    public HashSet<string> Classes { get; } = new();
    public Dictionary<string, string> Attributes { get; } = new();
    public MdElement Element { get; set; } = MdElement.Undefined;
    public string? Content { get; private set; } = string.Empty;

    public IReadOnlyCollection<IMdNode> Children => ChildNodes;

    public IMdNode Parent { get; private set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdNode AddChildNode(MdElement element) => CreateChildNode(element);

    public IMdNode WithContent(string content) {
        if (ChildNodes.LastOrDefault() is not { Element: MdElement.Content } lastNode) CreateChildNode(MdElement.Content, content);
        else lastNode.Content += content;

        return this;
    }

    public IMdNode WithHtmlContent(string content) {
        CreateChildNode(MdElement.HtmlContent, content);
        return this;
    }

    public IMdNode WithClass(string className) {
        Classes.Add(className);
        return this;
    }
    public IMdNode WithAttribute(string key, string value) {
        Attributes.AddOrUpdate(key, value);
        return this;
    }


    private MdNode CreateChildNode(MdElement element, string? content = null) {
        var child = new MdNode {
            Element = element,
            Content = content
        };

        ChildNodes.Add(child);
        child.Parent = this;
        return child;
    }
}
