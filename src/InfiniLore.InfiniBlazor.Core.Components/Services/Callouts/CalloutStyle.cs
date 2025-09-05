// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components.Callouts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CalloutStyle(
    string Key,
    ICollection<string> AlternateKeys,
    string IconName,
    string CssContainer,
    string CssTitle,
    string CssBody
) : ICalloutStyle {
    public static CalloutStyle Empty => new(
        string.Empty,
        [],
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty
    );
}
