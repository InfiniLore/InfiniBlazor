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
public sealed partial class HeadingSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^(?<level>\#{1,6})[\ ]+(?<text>[^\n]+)$", DefaultMultiLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int HLevelId = RegexRule.GroupNumberFromName("level");
    private static readonly int HTextId = RegexRule.GroupNumberFromName("text");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string headerText = match.Groups[HTextId].Value;
        int headingLevel = match.Groups[HLevelId].Length;

        HeadingMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingMdSyntaxNode>.Shared.Get();
        headingNode.WithLevel(headingLevel);
        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerText, headingNode);
    }
}
