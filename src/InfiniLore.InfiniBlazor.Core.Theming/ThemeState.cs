// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// --------------------------------------------------------------------------------------------------------------------
public record ThemeState : IThemeState {
    public string? CollectionName { get; set; }
    public string? ModeName { get; set; }
    public bool IsBinaryCollection { get; set; }
    public string? FirstModeInCollectionName { get; set; }
}
