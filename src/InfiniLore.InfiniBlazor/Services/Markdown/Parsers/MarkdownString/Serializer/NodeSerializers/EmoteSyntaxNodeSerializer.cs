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
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Emote)]
public sealed class EmoteSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int EmoteBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.EmoteContent);
    private static readonly int EmoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.Emote);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        if (entireMatch.Groups[EmoteId] is not {Success: true, Value: var originalEmote}) return ;
        if (entireMatch.Groups[EmoteBodyId] is not {Success: true, Value: var emoteBody}) return ;
        
        EmoteMdSyntaxNode node = EmoteMdSyntaxNode.Pool.Get();
        node.OriginalEmote = originalEmote;
        node.EmoteKey = emoteBody;
        parentNode.AddChildNode(node);
    }
}
