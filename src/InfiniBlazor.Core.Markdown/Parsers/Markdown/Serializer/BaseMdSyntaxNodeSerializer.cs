// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseMdSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    protected abstract Regex Syntax { get; }
    public virtual char[] TriggerCharacters { get; } = Array.Empty<char>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetMatch(string input, [NotNullWhen(true)] out Match? match, int startPosition = 0) {
        match = null;
        if (startPosition >= input.Length) return false;
        match = Syntax.Match(input, startPosition);
        return match.Success;
    }

    public abstract void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match);
}
