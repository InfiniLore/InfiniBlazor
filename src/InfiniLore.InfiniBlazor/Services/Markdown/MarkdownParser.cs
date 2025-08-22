// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser([FromKeyedServices("styled")] IHtmlStringMdSyntaxTreeParser styledHtmlString) : IMarkdownParser {
    [Inject] public IHtmlStringMdSyntaxTreeParser HtmlString { get; set; } = null!;
    public IHtmlStringMdSyntaxTreeParser StyledHtmlString => styledHtmlString;
    [Inject] public IMarkdownStringMdSyntaxTreeParser MarkdownString { get; set; } = null!;
    [Inject] public IXmlMdSyntaxTreeParser Xml { get; set; } = null!;
}