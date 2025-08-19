// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IXmlMdSyntaxNodeVisitor {
    void SerializeNode(IMdSyntaxNode node, XElement parentElement);
    void DeserializeNode(XElement element, IMdSyntaxNode parentNode);
}
