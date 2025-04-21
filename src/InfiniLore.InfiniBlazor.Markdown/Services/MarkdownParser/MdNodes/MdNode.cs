// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode {
    public MdElement Element { get; set; } = MdElement.Undefined;
    public string? Content { get; private set; } = string.Empty;

    public List<IMdNode> Children { get; } = new();
    public IMdNode Parent { get; private set; } = null!;
    public HashSet<string> Classes { get; } = new();
    public Dictionary<string, string> Attributes { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdNode AddChildNode(MdElement element) {
        return CreateChildNode(element);
    }

    public IMdNode WithContent(string content) {
        CreateChildNode(MdElement.Content, content);
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
        Children.Add(child);
        child.Parent = this;
        return child;
    }

}
