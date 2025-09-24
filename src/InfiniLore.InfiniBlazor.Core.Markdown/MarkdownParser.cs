// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Html;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Json;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

namespace InfiniLore.InfiniBlazor.Markdown;
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