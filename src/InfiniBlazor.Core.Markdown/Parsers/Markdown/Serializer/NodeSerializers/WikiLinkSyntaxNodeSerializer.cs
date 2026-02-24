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
public sealed partial class WikiLinkSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {

    [GeneratedRegex(@"\G\[\[(?<href>[^\]\[\ ]+)\]\]", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int WikiLinkHrefId = RegexRule.GroupNumberFromName("href");

    public override char[] TriggerCharacters { get; } = ['['];
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string href = match.Groups[WikiLinkHrefId].Value;

        WikiLinkMdSyntaxNode node = MdSyntaxNodePool<WikiLinkMdSyntaxNode>.Shared.Get();
        node.WithContent(href);
        parentNode.AddChildNode(node);
    }
}
