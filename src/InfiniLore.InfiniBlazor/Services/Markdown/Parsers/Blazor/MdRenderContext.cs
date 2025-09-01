// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdRenderContext {
    public event Func<IMdSyntaxNode, Task>? OnSyntaxNodeChange;
    public static MdRenderContext Empty { get; set; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InvokeSyntaxNodeChanged(IMdSyntaxNode node) {
        if (OnSyntaxNodeChange is null) return;
        await OnSyntaxNodeChange(node).ConfigureAwait(false);
    }
}
