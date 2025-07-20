// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
namespace InfiniLore.InfiniBlazor.Dialogs;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record DialogData(
    Type ComponentType,
    Dictionary<string, object?> Parameters
) : IDialogData {
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static DialogData Create<T>(Dictionary<string, object?>? parameters = null) where T : InfiniDialogBase => new(
        typeof(T),
        parameters ?? new Dictionary<string, object?>()
    );
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool IsValidType() {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ComponentType == null) return false;
        if (!ComponentType.IsSubclassOf(typeof(InfiniDialogBase))) return false;
        return true;
    }
}
