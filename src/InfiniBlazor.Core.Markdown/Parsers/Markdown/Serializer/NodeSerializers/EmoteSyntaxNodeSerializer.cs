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
public partial class EmoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"(?<emote>:(?<e>[\p{L}\p{N}_]+):)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int EmoteBodyId = Syntax.GroupNumberFromName(MdRegexGroupNames.EmoteContent);
    private static readonly int EmoteId = Syntax.GroupNumberFromName(MdRegexGroupNames.Emote);
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
        if (match.Groups[EmoteId] is not { Success: true, Value: var originalEmote }) return;
        if (match.Groups[EmoteBodyId] is not { Success: true, Value: var emoteBody }) return;

        EmoteMdSyntaxNode node = MdSyntaxNodePool<EmoteMdSyntaxNode>.Shared.Get();
        node.WithOriginalEmote(originalEmote)
            .WithEmoteKey(emoteBody);
        parentNode.AddChildNode(node);
    }
}
