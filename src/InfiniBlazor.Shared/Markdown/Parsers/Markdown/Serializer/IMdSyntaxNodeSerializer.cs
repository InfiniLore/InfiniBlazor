// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeSerializer {
    Match Match(string input, int startPosition = 0);
    void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match);
}
