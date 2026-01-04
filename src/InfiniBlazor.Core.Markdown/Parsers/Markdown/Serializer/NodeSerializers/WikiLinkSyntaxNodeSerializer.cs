// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class WikiLinkSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    private static readonly int WikiLinkHrefId = MdRegexLib.GetGroupId(MdRegexGroupNames.WikiLinkHref);

    public Regex Syntax { get; } = MdRegexLib.WikiLinkRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[WikiLinkHrefId].TryGetValue(out string? href)) return;

        WikiLinkMdSyntaxNode node = MdSyntaxNodePool<WikiLinkMdSyntaxNode>.Shared.Get();
        node.WithContent(href);
        parentNode.AddChildNode(node);
    }
}
