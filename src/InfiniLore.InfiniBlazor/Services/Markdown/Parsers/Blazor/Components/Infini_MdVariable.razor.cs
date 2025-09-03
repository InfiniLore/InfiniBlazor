// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public partial class Infini_MdVariable(IVariableContentProvider? variableContentProvider = null) {
    private IVariableContentProvider? VariableContentProvider { get; } = variableContentProvider;
}
