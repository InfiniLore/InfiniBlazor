// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<TableMdSyntaxNode> {
    private const string Alignments = nameof(TableMdSyntaxNode.Alignments);
    private const string Alignment = nameof(Alignment);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(TableMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        
        // ReSharper disable once InvertIf
        if (node.HasAlignments) {
            XElement alignmentsElement = new(Alignments);
            int columCount = node.GetHeaderCells().Length;
            foreach (TableMdSyntaxNode.Alignment alignment in node.Alignments.AsSpan(0, columCount)) {
                alignmentsElement.Add(new XElement(Alignment, Enum.GetName(alignment)));
            }
            targetElement.Add(alignmentsElement);
        }
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, TableMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        
        // ReSharper disable once InvertIf
        if (element.Element(Alignments) is {} alignmentsElement) {
            XElement[] alignmentElements = alignmentsElement.Elements(Alignment).ToArray();
            if (alignmentElements.Length <= 0) return;

            Span<TableMdSyntaxNode.Alignment> alignmentValues = stackalloc TableMdSyntaxNode.Alignment[alignmentElements.Length];
            for (int i = 0; i < alignmentElements.Length; i++) {
                alignmentValues[i] = Enum.Parse<TableMdSyntaxNode.Alignment>(alignmentElements[i].Value);
            }
            targetNode.WithAlignments(alignmentValues);
        }
    }
}
