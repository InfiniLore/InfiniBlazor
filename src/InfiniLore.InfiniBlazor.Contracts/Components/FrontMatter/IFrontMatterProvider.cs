// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components.FrontMatter;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IFrontMatterProvider {
    bool TryGetIconName(string key, [NotNullWhen(true)] out string? icon);
    
    bool TryParse(string? value, string? lang, [NotNullWhen(true)] out IEnumerable<IFrontMatterEntry>? entries);
}
