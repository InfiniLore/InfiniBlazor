// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public partial class Infini_MdTemplate(ITemplateContentProvider? templateContentProvider = null) {
    private ITemplateContentProvider? TemplateContentProvider { get; } = templateContentProvider;
}
