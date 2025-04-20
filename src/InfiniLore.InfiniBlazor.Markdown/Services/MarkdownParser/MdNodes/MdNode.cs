// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNode : IMdNode {
    public MdElement Element { get; set; } = MdElement.Undefined;
    public string Content { get; private set; } = string.Empty;
    
    public List<IMdNode> Children { get; } = new();
    public IMdNode Parent { get; private set; } = null!;
    public HashSet<string> Classes { get; } = new();
    public Dictionary<string, string> Attributes { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdNode AddChild(MdElement element) {
        var child = new MdNode {
            Element = element,
        };
        Children.Add(child);
        child.Parent = this;
        return child;
    }
    
    public IMdNode WithContent(string content) {
        var child = new MdNode {
            Element = MdElement.Content,
            Content = content,
        };
        Children.Add(child);
        child.Parent = this;
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
}
