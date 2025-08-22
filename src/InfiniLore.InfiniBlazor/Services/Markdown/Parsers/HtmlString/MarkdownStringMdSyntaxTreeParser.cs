// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString.SyntaxDeserializer;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IHtmlStringMdSyntaxTreeParser>]
public class HtmlStringMdSyntaxTreeParser(IHtmlStringMdSyntaxNodeVisitor nodeVisitor) : BaseHtmlStringMdSyntaxDeserializer(nodeVisitor), IHtmlStringMdSyntaxTreeParser;

[InjectableSingleton<IHtmlStringMdSyntaxTreeParser>("styled")]
public class StyledHtmlStringMdSyntaxTreeParser([FromKeyedServices("styled")] IHtmlStringMdSyntaxNodeVisitor nodeVisitor) : BaseHtmlStringMdSyntaxDeserializer(nodeVisitor), IHtmlStringMdSyntaxTreeParser;