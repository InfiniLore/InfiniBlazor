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
public sealed partial class HorizontalRuleSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^(?<hr>\ *?(\-{3,}?|_{3,}?)\ *?)$", DefaultMultiLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    public override ReadOnlySpan<char> TriggerCharacters => ['-', ' ', '_'];

    private static readonly int HrId = RegexRule.GroupNumberFromName("hr");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string hrContent = match.Groups[HrId].Value;

        HorizontalRuleMdSyntaxNode node = MdSyntaxNodePool<HorizontalRuleMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(hrContent);
        parentNode.AddChildNode(node);
    }
}
