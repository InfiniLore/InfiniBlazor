// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Xml.Linq;

namespace InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IXmlMdSyntaxNodeVisitor {
    XElement DeserializeToXml(IMdSyntaxNode node, XElement parentElement);
    IMdSyntaxNode SerializeToNode(IMdSyntaxTree tree, XElement element, IMdSyntaxNode parentNode);
}
