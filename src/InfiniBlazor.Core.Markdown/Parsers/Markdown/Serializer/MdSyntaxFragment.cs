// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct MdSyntaxFragment(IMdSyntaxNode? ParentNode, IMdSyntaxNode? ChildNode, Match? Match) {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxFragment AsUnhandledMatch(Match match, IMdSyntaxNode node)
        => new(node, null, match);

    public static MdSyntaxFragment AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => new(parentNode, childNode, null);
}
