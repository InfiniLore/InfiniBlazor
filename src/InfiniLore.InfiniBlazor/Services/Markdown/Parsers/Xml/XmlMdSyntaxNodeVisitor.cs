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
    public XElement SerializeNode(IMdSyntaxNode node, XElement parentElement) {
        var nodeElement = new XElement(node.Type.Name);
        parentElement.Add(nodeElement);
        SerializeDetails(Unsafe.As<TNode>(node), nodeElement);
        return nodeElement;   
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
    
    protected virtual void SerializeDetails(TNode node, XElement targetElement) {
        if (node.TryGetModifier(out IMdSyntaxNodeModifier? modifier)) targetElement.Add(SerializeModifiers(modifier));
    }

    public IMdSyntaxNode DeserializeNode(XElement element, IMdSyntaxNode parentNode) {
        TNode node = MdSyntaxNode<TNode>.Pool.Get();
        parentNode.AddChildNode(node);
        
        DeserializeDetails(element, node);
        return node;  
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

    protected virtual void DeserializeDetails(XElement element, TNode targetNode) {
        if (element.Element(Modifiers) is {} modifiersElement) targetNode.WithModifier(DeserializeModifiers(modifiersElement));
    }
}
