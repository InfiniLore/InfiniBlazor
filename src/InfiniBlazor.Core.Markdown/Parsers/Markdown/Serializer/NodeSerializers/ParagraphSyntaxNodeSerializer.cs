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
public sealed partial class ParagraphSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {

    [GeneratedRegex(@"\G^(?<p>.+?)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int PId = RegexRule.GroupNumberFromName("p");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string paragraph = match.Groups[PId].Value;

        if (parentNode is HtmlSpanMdSyntaxNode) {
            stack.PushSingleLineMatchesToStack(paragraph, parentNode);
            return;
        }

        ParagraphMdSyntaxNode node = MdSyntaxNodePool<ParagraphMdSyntaxNode>.Shared.Get();
        parentNode = parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(paragraph, parentNode);
    }
}
