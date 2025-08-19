// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record MdEditorOutput(
    MdSyntaxTree? SyntaxTree,
    string? StringPreview,
    MarkupString MarkupPreview
) {
    public static MdEditorOutput Empty => new(null, null, default);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetSyntaxTree([NotNullWhen(true)] out IMdSyntaxTree? syntaxTree) {
        syntaxTree = SyntaxTree;
        return syntaxTree != null;
    }
    
    public bool TryGetStringPreview([NotNullWhen(true)] out string? stringPreview) {
        stringPreview = StringPreview;
        return stringPreview != null;
    }
    
    public bool TryGetMarkupPreview(out MarkupString markupPreview) {
        markupPreview = MarkupPreview;
        return markupPreview.Value.IsNotNullOrWhiteSpace();
    }
}
