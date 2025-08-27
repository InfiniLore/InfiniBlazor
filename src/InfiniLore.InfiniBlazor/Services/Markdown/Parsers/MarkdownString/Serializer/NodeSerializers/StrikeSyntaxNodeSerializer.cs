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
public static class StrikeSyntaxNodeSerializer  {
    private static readonly int StrikeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.StrikeContent);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[StrikeContentId].TryGetValue(out string? strikeValue)) return ;
        
        StrikeMdSyntaxNode node = StrikeMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        
        stack.PushSingleLineMatchesToStack(strikeValue, node);
    }
}
