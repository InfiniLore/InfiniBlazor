// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Strike)]
public sealed class StrikeSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int StrikeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.StrikeContent);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        if (!entireMatch.Groups[StrikeContentId].TryGetValue(out string? strikeValue)) return ;
        
        StrikeMdSyntaxNode node = StrikeMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        
        stack.PushSingleLineMatchesToStack(strikeValue, node);
    }
}
