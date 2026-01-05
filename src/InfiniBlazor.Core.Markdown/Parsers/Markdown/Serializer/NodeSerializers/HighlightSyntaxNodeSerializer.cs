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
public partial class HighlightSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"==(?<h>.+?)(?<!\\)==", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int HId = Syntax.GroupNumberFromName("h");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[HId].TryGetValue(out string? boldValue)) return;

        HighlightMdSyntaxNode node = MdSyntaxNodePool<HighlightMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(boldValue, node);
    }
}
