// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IXmlMdSyntaxNodeVisitor {
    XElement DeserializeFromNode(IMdSyntaxNode node, XElement parentElement);
    IMdSyntaxNode SerializeToNode(XElement element, IMdSyntaxNode parentNode);
}
