// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct MdEditorOutput(
    IMdSyntaxTree? SyntaxTree,
    string? StringPreview,
    MarkupString MarkupPreview
) {
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
