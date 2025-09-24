// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Html;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Json;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParser {
    IHtmlMdSyntaxTreeParser Html { get; }
    IMarkdownMdSyntaxTreeParser Markdown { get; }
    IXmlMdSyntaxTreeParser Xml { get; }
    IJsonMdSyntaxTreeParser Json { get; }
}
