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
    string ExpectedStringOutput,
    
    // ReSharper disable once NotAccessedPositionalProperty.Global
    Action<IMdNode>? ConfigureExpectedNode = null
) {
    public IMdNode? ExpectedNode { get; } = CreateNode(ConfigureExpectedNode);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static MdNode? CreateNode(Action<IMdNode>? configureNode) {
        if (configureNode == null) return null;
        var node = new MdNode();
        configureNode.Invoke(node);
        return node;
    }
}
