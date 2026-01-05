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
public partial class FootnoteReferenceSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\[\^(?<id>[\d\p{L}\p{N}]+)\]", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int FootnoteIdentifierId = Syntax.GroupNumberFromName("id");
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

        FootnoteReferenceMdSyntaxNode node = MdSyntaxNodePool<FootnoteReferenceMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);
    }
}
