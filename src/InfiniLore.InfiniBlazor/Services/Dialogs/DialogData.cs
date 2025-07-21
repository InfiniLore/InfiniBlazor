// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;

namespace InfiniLore.InfiniBlazor.Dialogs;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract record DialogData<T> : IDialogData where T : InfiniDialogBase {
    public Type ComponentType { get; } = typeof(T);
    public Guid Id { get; } = Guid.CreateVersion7();
    
    private readonly int _priority;
    public int Priority {
        get => _priority; 
        init => _priority = Math.Max(0, value);
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public abstract IDictionary<string, object?>? AsDynamicParameters();
}
