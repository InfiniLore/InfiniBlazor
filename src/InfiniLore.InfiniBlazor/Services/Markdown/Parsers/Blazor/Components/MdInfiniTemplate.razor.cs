// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public partial class MdInfiniTemplate(ITemplateContentProvider? templateContentProvider = null) {
    private ITemplateContentProvider? TemplateContentProvider { get; } = templateContentProvider;
}
