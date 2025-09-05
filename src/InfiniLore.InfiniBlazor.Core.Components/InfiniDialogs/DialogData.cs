// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.Dialogs;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract record DialogData<TComponent> : IDialogData where TComponent : InfiniDialogBase {
    public Type ComponentType { get; } = typeof(TComponent);
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
