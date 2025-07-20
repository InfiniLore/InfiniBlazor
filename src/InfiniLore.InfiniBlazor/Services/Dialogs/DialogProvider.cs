// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Dialogs;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IDialogProvider>]
public class DialogProvider(ILogger<DialogProvider> logger) : IDialogProvider {
    public Action? DialogOpened { get; set; } = null;
    public Func<Task>? DialogOpenedAsync { get; set; } = null;

    private ConcurrentStack<IDialogData> Dialogs { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void AddDialog(IDialogData dialog) {
        if (Dialogs.Contains(dialog)) {
            logger.Warning("Dialog already exists: {dialog}", dialog);
            return;       
        }

        if (!dialog.IsValidType()) {
            logger.Warning("Invalid dialog type: {dialog}", dialog);
            return;      
        }
        
        Dialogs.Push(dialog);
        DialogOpened?.Invoke();
        _ = DialogOpenedAsync?.Invoke().ConfigureAwait(false);
    }
    
    public bool TryPopDialog([NotNullWhen(true)] out IDialogData? dialogData) 
        => Dialogs.TryPop(out dialogData);
}
