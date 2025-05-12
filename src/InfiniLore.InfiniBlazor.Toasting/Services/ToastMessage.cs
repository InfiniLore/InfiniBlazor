// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ToastMessage(
    string IconName,
    string Title,
    string Message,
    int DurationSeconds
) : IToastMessage {
    public Guid Id { get; } = Guid.NewGuid();
    public string TailwindData { get; set; } = "bg-green-100 border-green-500  text-green-900";
}
