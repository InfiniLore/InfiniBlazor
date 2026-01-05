// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class HeadingSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"(?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\n]+)$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int HLevelId = Syntax.GroupNumberFromName(MdRegexGroupNames.HeadingLevel);
    private static readonly int HTextId = Syntax.GroupNumberFromName(MdRegexGroupNames.HeadingText);
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
        if (!match.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!match.Groups[HTextId].TryGetValue(out string? headerText)) return;

        HeadingMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingMdSyntaxNode>.Shared.Get();
        headingNode.WithLevel(headingLevel);
        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerText, headingNode);
    }
}
