// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteMdSyntaxNode() : MdSyntaxNode<EmoteMdSyntaxNode>(initialChildCount: 0) {
    public string EmoteKey { get; private set; } = string.Empty;
    public string OriginalEmote { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public EmoteMdSyntaxNode WithEmoteKey(string emoteKey) {
        EmoteKey = emoteKey;
        return this;
    }
    public EmoteMdSyntaxNode WithOriginalEmote(string originalEmote) {
        OriginalEmote = originalEmote;
        return this;
    }

    public override bool TryReset() {
        EmoteKey = string.Empty;
        OriginalEmote = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(EmoteMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(EmoteKey, other.EmoteKey)
            && StringComparer.Ordinal.Equals(OriginalEmote, other.OriginalEmote);
}
