// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastMessageData(
    string Title,
    int DurationSeconds,
    IToastAppearance Appearance,
    string? Body = null,
    string? LinkHref = null,
    string? LinkTitle = null
) : IToastMessageData {
    public Guid Id { get; init; } = Guid.NewGuid();
}
