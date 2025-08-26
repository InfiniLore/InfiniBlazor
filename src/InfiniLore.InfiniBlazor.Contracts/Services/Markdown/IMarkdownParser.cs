// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParser {
    IHtmlStringMdSyntaxTreeParser HtmlString { get; }
    IHtmlStringMdSyntaxTreeParser StyledHtmlString { get; }
    IMarkdownStringMdSyntaxTreeParser MarkdownString { get; }
    IXmlMdSyntaxTreeParser Xml { get; }
}
