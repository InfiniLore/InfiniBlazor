// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct MdSyntaxFragment(IMdSyntaxNode? ParentNode, IMdSyntaxNode? ChildNode, Match? Match, IMdSyntaxNodeSerializer? NodeSerializer) {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxFragment AsUnhandledMatch(Match match, IMdSyntaxNode node, IMdSyntaxNodeSerializer nodeSerializer)
        => new(node, null, match, nodeSerializer);

    public static MdSyntaxFragment AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => new(parentNode, childNode, null, null);
}
