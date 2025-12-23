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
public class StrikeSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    private static readonly int StrikeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.StrikeContent);
    
    public Regex Syntax { get; } = MdRegexLib.StrikeRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[StrikeContentId].TryGetValue(out string? strikeValue)) return;

        StrikeMdSyntaxNode node = StrikeMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);

        stack.PushSingleLineMatchesToStack(strikeValue, node);
    }
}
