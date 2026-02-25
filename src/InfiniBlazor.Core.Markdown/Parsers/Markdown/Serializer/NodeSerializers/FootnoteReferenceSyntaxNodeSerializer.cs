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
public sealed partial class FootnoteReferenceSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G\[\^(?<id>[\d\p{L}\p{N}]+)\]", DefaultSingleLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['['];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int FootnoteIdentifierId = RegexRule.GroupNumberFromName("id");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string footnoteId = match.Groups[FootnoteIdentifierId].Value;

        FootnoteReferenceMdSyntaxNode node = MdSyntaxNodePool<FootnoteReferenceMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);
    }
}
