// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MarkdownParserExtensions {
    public static string FromMarkdownStringToHtmlString(this IMarkdownParser parser, string input) {
        using IMdSyntaxTree tree = parser.MarkdownString.SerializeToSyntaxTree(input);
        return parser.HtmlString.DeserializeToString(tree);
    }
}