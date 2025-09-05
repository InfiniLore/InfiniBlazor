// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Json;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser(
    IHtmlStringMdSyntaxTreeParser htmlString,
    [FromKeyedServices("styled")] IHtmlStringMdSyntaxTreeParser styledHtmlString,
    IMsStringMdSyntaxTreeParser markdownString,
    IXmlMdSyntaxTreeParser xml,
    IJsonMdSyntaxTreeParser json
) : IMarkdownParser {
    public IHtmlStringMdSyntaxTreeParser HtmlString => htmlString;
    public IHtmlStringMdSyntaxTreeParser StyledHtmlString => styledHtmlString;
    public IMsStringMdSyntaxTreeParser MarkdownString => markdownString;
    public IXmlMdSyntaxTreeParser Xml => xml;
    public IJsonMdSyntaxTreeParser Json => json;
}