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
public partial class ParagraphSyntaxNodeSerializer : IMdSyntaxNodeSerializer{

    [GeneratedRegex("^(?<p>.+?)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int PId = Syntax.GroupNumberFromName("p");
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
