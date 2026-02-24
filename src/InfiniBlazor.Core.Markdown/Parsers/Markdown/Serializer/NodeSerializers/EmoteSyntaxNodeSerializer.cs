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
public sealed partial class EmoteSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G:(?<e>[\p{L}\p{N}_]+):", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = [':'];
    public override char[] TriggerCharacters => STriggerCharacters;

    private static readonly int EmoteBodyId = RegexRule.GroupNumberFromName("e");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
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
