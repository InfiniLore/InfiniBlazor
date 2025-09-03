// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteMdSyntaxNode() : MdSyntaxNode<EmoteMdSyntaxNode>(initialChildCount: 0) {
    public string EmoteKey { get; set; } = string.Empty;
    public string OriginalEmote { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        EmoteKey = string.Empty;
        OriginalEmote = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(EmoteMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(EmoteKey, other.EmoteKey)
            && StringComparer.InvariantCulture.Equals(OriginalEmote, other.OriginalEmote);
}
