// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Dialogs;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IDialogProvider {
    Action? DialogOpened { get; set; }
    Func<Task>? DialogOpenedAsync { get; set; }

    void AddDialog(IDialogData dialog);
    
    bool TryPopDialog([NotNullWhen(true)] out IDialogData? dialogData);
}
