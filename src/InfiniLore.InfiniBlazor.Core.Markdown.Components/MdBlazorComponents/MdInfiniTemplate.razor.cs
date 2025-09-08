// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;

namespace InfiniLore.InfiniBlazor.Markdown.MdBlazorComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MdInfiniTemplate(ITemplateDataProvider? templateContentProvider = null) {
    private ITemplateDataProvider? TemplateContentProvider { get; } = templateContentProvider;
}
