// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastingData(
    string Title,
    string? Body = null,
    string? LinkHref = null,
    string? LinkTitle = null
) : IToastingData {
    public Guid Id { get; private init; } = Guid.CreateVersion7();
    
    public static ToastingData Empty { get; } = new(string.Empty) {
        Id = Guid.Empty
    };
}
