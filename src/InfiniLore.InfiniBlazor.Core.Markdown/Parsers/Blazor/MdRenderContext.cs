// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdRenderContext {
    public event Func<IMdSyntaxNode, Task>? OnSyntaxNodeChanged;
    public static MdRenderContext Empty { get; set; } = new MdRenderContext();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InvokeSyntaxNodeChange(IMdSyntaxNode node) {
        if (OnSyntaxNodeChanged is null) return;
        await OnSyntaxNodeChanged(node).ConfigureAwait(false);
    }
}
