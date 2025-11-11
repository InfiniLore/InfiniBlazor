// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Html;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IHtmlMdSyntaxTreeParser {
    Task<string> DeserializeToStringAsync(IMdSyntaxTree tree, CancellationToken ct = default);
}
