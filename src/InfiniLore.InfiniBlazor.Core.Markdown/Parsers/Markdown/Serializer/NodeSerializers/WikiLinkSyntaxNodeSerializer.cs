// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class WikiLinkSyntaxNodeSerializer  {
    private static readonly int WikiLinkHrefId = MdRegexLib.GetGroupId(MdRegexGroupNames.WikiLinkHref);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[WikiLinkHrefId].TryGetValue(out string? href)) return ;

        WikiLinkMdSyntaxNode node = WikiLinkMdSyntaxNode.Pool.Get();
        node.WithContent(href);
        parentNode.AddChildNode(node);
    }
}
