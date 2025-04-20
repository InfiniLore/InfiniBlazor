// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record MarkdownTestDto(
    string Section,
    string Markdown,
    string HtmlOutput,
    Action<IMdNode>? ConfigureNode = null
) {
    public IMdNode? Node { get; } = CreateNode(ConfigureNode); 
    private static IMdNode? CreateNode(Action<IMdNode>? configureNode) {
        if (configureNode == null) return null;
        var node = new MdNode();
        configureNode.Invoke(node);
        return node;
    }
}
