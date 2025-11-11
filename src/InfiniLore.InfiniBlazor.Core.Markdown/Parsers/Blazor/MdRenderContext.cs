// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdRenderContext {
    public bool IsInteractive { get; set; }
    public event Func<IMdSyntaxNode, Task>? OnSyntaxNodeChanged;
    public static MdRenderContext Empty { get; set; } = new();
    public static MdRenderContext Interactive => new() { IsInteractive = true };
    public static MdRenderContext NonInteractive => new() { IsInteractive = false };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InvokeSyntaxNodeChange(IMdSyntaxNode node) {
        if (OnSyntaxNodeChanged is null) return;

        await OnSyntaxNodeChanged(node).ConfigureAwait(false);
    }
}
