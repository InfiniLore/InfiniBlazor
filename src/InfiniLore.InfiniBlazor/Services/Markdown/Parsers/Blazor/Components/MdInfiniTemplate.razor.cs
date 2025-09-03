// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MdInfiniTemplate(ITemplateContentProvider? templateContentProvider = null) {
    private ITemplateContentProvider? TemplateContentProvider { get; } = templateContentProvider;
}
