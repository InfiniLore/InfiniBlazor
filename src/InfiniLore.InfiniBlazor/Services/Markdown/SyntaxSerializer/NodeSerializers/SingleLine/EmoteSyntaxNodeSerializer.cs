// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Emote)]
public sealed class EmoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int EmoteBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.EmoteContent);
    private static readonly int EmoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.Emote);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Emote;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (entireMatch.Groups[EmoteId] is not {Success: true, Value: var originalEmote}) return ;
        if (entireMatch.Groups[EmoteBodyId] is not {Success: true, Value: var emoteBody}) return ;
        
        EmoteMdSyntaxNode node = EmoteMdSyntaxNode.Pool.Get();
        node.OriginalEmote = originalEmote;
        node.EmoteKey = emoteBody;
        parentNode.AddChildNode(node);
    }
}
