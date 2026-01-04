// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace InfiniBlazor.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class XmlMdSyntaxNodeVisitor<TNode> : IXmlMdSyntaxNodeVisitor where TNode : MdSyntaxNode<TNode>, new() {
    private const string Modifiers = nameof(Modifiers);
    private const string OriginalInput = nameof(OriginalInput);
    private const string Attributes = nameof(Attributes);
    private const string Start = nameof(Start);
    private const string End = nameof(End);
    private const string Key = nameof(Key);
    private const string Entry = nameof(Entry);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public XElement DeserializeToXml(IMdSyntaxNode node, XElement parentElement) {
        var nodeElement = new XElement(node.Type.Name);
        parentElement.Add(nodeElement);
        DeserializeDetails(Unsafe.As<TNode>(node), nodeElement);
        return nodeElement;
    }

    private static XElement SerializeModifiers(IMdSyntaxNodeModifier modifiers) {
        var modifierElement = new XElement(Modifiers);
        modifierElement.Add(new XElement(Attributes,
            modifiers.Attributes.Select(attr => new XElement(
                Entry,
                new XAttribute(Key, attr.Key),
                new XAttribute(Start, attr.Value.Start.Value),
                new XAttribute(End, attr.Value.End.Value)
            ))));
        modifierElement.Add(new XElement(OriginalInput) {
            Value = modifiers.OriginalInput
        });
        return modifierElement;
    }

    protected virtual void DeserializeDetails(TNode node, XElement targetElement) {
        if (node.Modifier is {} modifier) targetElement.Add(SerializeModifiers(modifier));
    }

    protected void AddXmlPreserveSpace(XElement element) => element.SetAttributeValue(XNamespace.Xml + "space", "preserve");

    public IMdSyntaxNode SerializeToNode(IMdSyntaxTree tree, XElement element, IMdSyntaxNode parentNode) {
        TNode node = MdSyntaxNodePool<TNode>.Shared.Get();
        parentNode.AddChildNode(node);

        SerializeDetails(tree, element, node);
        return node;
    }

    private static MdSyntaxNodeModifier DeserializeModifiers(XElement element)
        => MdSyntaxNodeModifier.FromString(element.Element(OriginalInput)!.Value);

    protected virtual void SerializeDetails(IMdSyntaxTree tree, XElement element, TNode targetNode) {
        if (element.Element(Modifiers) is {} modifiersElement) targetNode.WithModifier(DeserializeModifiers(modifiersElement));
    }
}
