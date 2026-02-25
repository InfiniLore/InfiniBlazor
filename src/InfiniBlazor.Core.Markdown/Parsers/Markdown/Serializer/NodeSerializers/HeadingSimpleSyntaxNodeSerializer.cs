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
public sealed partial class HeadingSimpleSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^(?<text>.+?)\n(?<id>[\ ]*(?:={3,}?|-{3,}?)[\ ]*$)", DefaultMultiLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int HsTextId = RegexRule.GroupNumberFromName("text");
    private static readonly int HsIdentifierId = RegexRule.GroupNumberFromName("id");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string headerSimpleText = match.Groups[HsTextId].Value;
        string headerIdentifierText = match.Groups[HsIdentifierId].Value;

        HeadingSimpleMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingSimpleMdSyntaxNode>.Shared.Get();
        headingNode.WithIdentifier(headerIdentifierText);

        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode);
    }
}
