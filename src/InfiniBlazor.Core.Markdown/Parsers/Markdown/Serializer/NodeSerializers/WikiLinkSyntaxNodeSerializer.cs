// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class WikiLinkSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    
    [GeneratedRegex(@"\[\[(?<href>[^\]\[\ ]+)\]\]", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int WikiLinkHrefId = Syntax.GroupNumberFromName("href");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
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
