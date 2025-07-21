// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Dialogs;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IDialogProvider {
    event Func<Task>? OnDialogOpenedAsync;

    Task PushDialogAsync(IDialogData dialog);
    Task<IDialogData?> TryRemoveDialogAsync(Guid id);
    
    Task<IDialogData?> TakeDialogAsync();
}
