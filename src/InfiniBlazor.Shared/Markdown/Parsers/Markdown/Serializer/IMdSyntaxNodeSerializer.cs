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
public interface IMdSyntaxNodeSerializer {
    char[] TriggerCharacters { get; }
    
    bool TryGetMatch(string input, [NotNullWhen(true)] out Match? match, int startPosition = 0);
    void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match);
}
