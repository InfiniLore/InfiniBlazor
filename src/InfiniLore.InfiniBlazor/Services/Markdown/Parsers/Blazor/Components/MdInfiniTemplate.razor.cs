// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MdInfiniTemplate(ITemplateDataProvider? templateDataProvider = null) {
    private ITemplateDataProvider? TemplateDataProvider { get; } = templateDataProvider;
}
