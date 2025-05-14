// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastMessage(
    string Title,
    int DurationSeconds,
    IToastAppearance Appearance,
    string? Body = null,
    string? LinkHref = null,
    string? LinkTitle = null
) : IToastMessage {
    public Guid Id { get; init; } = Guid.NewGuid();
}
