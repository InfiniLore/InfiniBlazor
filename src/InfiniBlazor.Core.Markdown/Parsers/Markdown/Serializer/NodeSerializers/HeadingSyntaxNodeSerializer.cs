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
public class HeadingSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    private static readonly int HLevelId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingLevel);
    private static readonly int HTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingText);
    
    public Regex Syntax { get; } = MdRegexLib.HeadingRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!match.Groups[HTextId].TryGetValue(out string? headerText)) return;

        HeadingMdSyntaxNode headingNode = HeadingMdSyntaxNode.Pool.Get();
        headingNode.WithLevel(headingLevel);
        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerText, headingNode);
    }
}
