// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Html;
using InfiniBlazor.Markdown.Parsers.Json;
using InfiniBlazor.Markdown.Parsers.Markdown;
using InfiniBlazor.Markdown.Parsers.Xml;

namespace InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParser {
    IHtmlMdSyntaxTreeParser Html { get; }
    IMarkdownMdSyntaxTreeParser Markdown { get; }
    IXmlMdSyntaxTreeParser Xml { get; }
    IJsonMdSyntaxTreeParser Json { get; }
}
