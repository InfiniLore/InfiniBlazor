// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HeadingSyntaxNodeSerializer  {
    private static readonly int HLevelId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingLevel);
    private static readonly int HTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingText);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!match.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!match.Groups[HTextId].TryGetValue(out string? headerText)) return;

        HeadingMdSyntaxNode headingNode = HeadingMdSyntaxNode.Pool.Get();
        headingNode.WithLevel(headingLevel);
        parentNode.AddChildNode(headingNode);
        
        stack.PushSingleLineMatchesToStack(headerText, headingNode);
    }
}
