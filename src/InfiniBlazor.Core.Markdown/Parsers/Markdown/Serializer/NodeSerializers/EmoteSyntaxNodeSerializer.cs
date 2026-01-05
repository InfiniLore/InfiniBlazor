// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class EmoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@":(?<e>[\p{L}\p{N}_]+):", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int EmoteBodyId = Syntax.GroupNumberFromName("e");
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
        string originalEmote = match.Value;
        string emoteBody = match.Groups[EmoteBodyId].Value;
        
        EmoteMdSyntaxNode node = MdSyntaxNodePool<EmoteMdSyntaxNode>.Shared.Get();
        node.WithOriginalEmote(originalEmote)
            .WithEmoteKey(emoteBody);
        parentNode.AddChildNode(node);
    }
}
