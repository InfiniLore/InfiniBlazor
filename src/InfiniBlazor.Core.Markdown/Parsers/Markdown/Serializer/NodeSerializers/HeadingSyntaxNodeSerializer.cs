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
public partial class HeadingSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"\G^(?<level>\#{1,6})[\ ]+(?<text>[^\n]+)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int HLevelId = Syntax.GroupNumberFromName("level");
    private static readonly int HTextId = Syntax.GroupNumberFromName("text");
    
    public char[] TriggerCharacters { get; } = Array.Empty<char>();
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
        string headerText = match.Groups[HTextId].Value;
        int headingLevel = match.Groups[HLevelId].Length;

        HeadingMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingMdSyntaxNode>.Shared.Get();
        headingNode.WithLevel(headingLevel);
        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerText, headingNode);
    }
}
