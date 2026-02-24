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
public sealed partial class FootnoteDescriptionSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^\[\^(?<id>[\d\p{L}\p{N}]+)\][\ ]?:[\ ]?(?<body>.+(?:\n(?!\[)(?:.+))*)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;
    
    public override char[] TriggerCharacters { get; } = ['['];
    
    private static readonly int FootnoteIdentifierId = RegexRule.GroupNumberFromName("id");
    private static readonly int FootnoteBodyId = RegexRule.GroupNumberFromName("body");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string footnoteId = match.Groups[FootnoteIdentifierId].Value;
        string body = match.Groups[FootnoteBodyId].Value;

        FootnoteDescriptionMdSyntaxNode node = MdSyntaxNodePool<FootnoteDescriptionMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);

        stack.PushMultiLineMatchesToStack(body, node);
    }
}
