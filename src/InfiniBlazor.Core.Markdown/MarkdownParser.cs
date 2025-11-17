// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.Markdown.Parsers.Html;
using InfiniBlazor.Markdown.Parsers.Json;
using InfiniBlazor.Markdown.Parsers.Markdown;
using InfiniBlazor.Markdown.Parsers.Xml;

namespace InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser(
    IHtmlMdSyntaxTreeParser html,
    IMarkdownMdSyntaxTreeParser markdownString,
    IXmlMdSyntaxTreeParser xml,
    IJsonMdSyntaxTreeParser json
) : IMarkdownParser {
    public IHtmlMdSyntaxTreeParser Html => html;
    public IMarkdownMdSyntaxTreeParser Markdown => markdownString;
    public IXmlMdSyntaxTreeParser Xml => xml;
    public IJsonMdSyntaxTreeParser Json => json;
}