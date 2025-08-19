// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class XmlMdSyntaxNodeVisitor<TNode> : IXmlMdSyntaxNodeVisitor where TNode : MdSyntaxNode<TNode>, new() {
    private const string Modifiers = nameof(Modifiers);
    private const string OriginalInput = nameof(OriginalInput);
    private const string Attributes = nameof(Attributes);
    private const string Start = nameof(Start);
    private const string End = nameof(End);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void SerializeNode(IMdSyntaxNode node, XElement parentElement) {
        if (node.TryGetModifier(out IMdSyntaxNodeModifier? modifier)) parentElement.Add(SerializeModifiers(modifier));
        
        var nodeElement = new XElement(node.Type.Name);
        parentElement.Add(nodeElement);
        
        SerializeDetails(Unsafe.As<TNode>(node), nodeElement);
    }
    
    private static XElement SerializeModifiers(IMdSyntaxNodeModifier modifiers) {
        var modifierElement = new XElement(Modifiers);
        modifierElement.Add(new XElement(Attributes,
            modifiers.Attributes.Select(attr => new XElement(
                attr.Key,
                new XAttribute(Start, attr.Value.Start.Value),
                new XAttribute(End, attr.Value.End.Value)
            ))));
        modifierElement.Add(new XElement(OriginalInput) {
            Value = modifiers.OriginalInput
        });
        return modifierElement;
    }

    protected virtual void SerializeDetails(TNode node, XElement targetElement){}

    public void DeserializeNode(XElement element, IMdSyntaxNode parentNode) {
        TNode node = MdSyntaxNode<TNode>.Pool.Get();
        parentNode.AddChildNode(node);
        
        if (element.Element(Modifiers) is {} modifiersElement) node.WithModifier(DeserializeModifiers(modifiersElement));
        DeserializeDetails(element, node);
    }

    private static IMdSyntaxNodeModifier DeserializeModifiers(XElement element) {
        MdSyntaxNodeModifier modifier = MdSyntaxNodeModifier.Pool.Get();
        
        foreach (XElement attr in element.Element(Attributes)!.Elements()) {
            // TODO - Validate that the attributes are valid
            int start = int.Parse(attr.Attribute(Start)!.Value);
            int end = int.Parse(attr.Attribute(End)!.Value);
            modifier.Attributes[attr.Name.LocalName] = new Range(start, end);
        }
        
        modifier.OriginalInput = element.Element(OriginalInput)!.Value;
        return modifier;   
    }
    
    protected virtual void DeserializeDetails(XElement element, TNode targetNode){}
}
