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
public partial class FootnoteDescriptionSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"^\[\^(?<id>[\d\p{L}\p{N}]+)\][\ ]?:[\ ]?(?<body>.+(?:\n(?!\[)(?:.+))*)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int FootnoteIdentifierId = Syntax.GroupNumberFromName("id");
    private static readonly int FootnoteBodyId = Syntax.GroupNumberFromName("body");
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
        if (match.Groups[FootnoteIdentifierId] is not { Success: true, Value: var footnoteId }) return;
        if (match.Groups[FootnoteBodyId] is not { Success: true, Value: var body }) return;

        FootnoteDescriptionMdSyntaxNode node = MdSyntaxNodePool<FootnoteDescriptionMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);

        stack.PushMultiLineMatchesToStack(body, node);
        stack.TreeReference.StoreChildAtCache(node);
    }
}
