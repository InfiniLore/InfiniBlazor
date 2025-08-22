// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
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
    IMarkdownStringMdSyntaxTreeParser markdownString,
    IXmlMdSyntaxTreeParser xml
) : IMarkdownParser {
    public IHtmlStringMdSyntaxTreeParser HtmlString => htmlString;
    public IHtmlStringMdSyntaxTreeParser StyledHtmlString => styledHtmlString;
    public IMarkdownStringMdSyntaxTreeParser MarkdownString => markdownString;
    public IXmlMdSyntaxTreeParser Xml => xml;
}