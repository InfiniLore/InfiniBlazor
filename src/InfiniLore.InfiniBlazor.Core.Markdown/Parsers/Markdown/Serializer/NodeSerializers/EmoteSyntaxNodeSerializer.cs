// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EmoteSyntaxNodeSerializer {
    private static readonly int EmoteBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.EmoteContent);
    private static readonly int EmoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.Emote);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (match.Groups[EmoteId] is not { Success: true, Value: var originalEmote }) return;
        if (match.Groups[EmoteBodyId] is not { Success: true, Value: var emoteBody }) return;

        EmoteMdSyntaxNode node = EmoteMdSyntaxNode.Pool.Get();
        node.WithOriginalEmote(originalEmote)
            .WithEmoteKey(emoteBody);
        parentNode.AddChildNode(node);
    }
}
