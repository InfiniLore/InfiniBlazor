// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MdInfiniTemplate(ITemplateDataProvider? templateContentProvider = null) {
    private ITemplateDataProvider? TemplateContentProvider { get; } = templateContentProvider;
}
