// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Collections.Concurrent;

namespace InfiniLore.InfiniBlazor.Dialogs;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IDialogProvider>]
public class DialogProvider(ILogger<DialogProvider> logger) : IDialogProvider {
    public event Func<Task>? DialogOpenedAsync;
    
    private ConcurrentDictionary<Guid, IDialogData> DialogsById { get; } = new();
    private int DialogCount { get; set; }
    private Guid[] DialogIds { get; set; } = ArrayPool<Guid>.Shared.Rent(0);
    private SemaphoreSlim DialogsSemaphore { get; } = new(1, 1);
    
    private Comparer<Guid>? _comparer;
    private Comparer<Guid> Comparer => _comparer ??= Comparer<Guid>.Create(
        // Could cause issues if used outside the semaphore lock thing, but is fine for now
        (x, y) => DialogsById[y].Priority.CompareTo(DialogsById[x].Priority)
    );
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task PushDialogAsync(IDialogData dialog) {
        if (DialogsById.ContainsKey(dialog.Id)) {
            logger.Warning("Dialog already exists: {dialog}", dialog);
            return;       
        }
        await DialogsSemaphore.WaitAsync();

        try {
            if (!DialogsById.TryAdd(dialog.Id, dialog)) {
                logger.Warning("Dialog could not be added: {dialog}", dialog);
                return;
            }
            
            DialogCount++;
            Guid[] newArray = ArrayPool<Guid>.Shared.Rent(DialogCount);
            DialogIds.CopyTo(newArray, 0);
            ArrayPool<Guid>.Shared.Return(DialogIds);
            DialogIds = newArray;
        
            DialogIds[Math.Max(0, DialogCount - 1)] = dialog.Id;
            SortDialogIdsByPriority();
            
            _ = DialogOpenedAsync?.Invoke();
            logger.Information("Dialog added: {dialog}", dialog);
        }
        finally {
            DialogsSemaphore.Release();       
        }
    }

    public async Task<IDialogData?> TryRemoveDialogAsync(Guid id) {
        await DialogsSemaphore.WaitAsync();
        try {
            if (!DialogsById.TryRemove(id, out IDialogData? dialog)) return null;

            Span<Guid> span = DialogIds.AsSpan(0, DialogCount);
            int foundIndex = span.IndexOf(dialog.Id);
            if (foundIndex == -1) return null; // Removed by a separate thread possibly? Should not happen
        
            Guid[] newArray = ArrayPool<Guid>.Shared.Rent(DialogCount - 1);
            span[..foundIndex].CopyTo(newArray);
            span[(foundIndex+1)..].CopyTo(newArray.AsSpan(foundIndex));
            ArrayPool<Guid>.Shared.Return(DialogIds);
            DialogIds = newArray;
        
            DialogCount--;
            SortDialogIdsByPriority();
            return dialog;
        }
        finally {
            DialogsSemaphore.Release();     
        }
    }

    private void SortDialogIdsByPriority() {
        
        logger.Information("Before sort: {priorities}", string.Join(", ", 
            DialogIds.Take(DialogCount).Select(id => DialogsById[id].Priority)));
        
        Array.Sort(DialogIds, 0, DialogCount, Comparer);
    
        logger.Information("After sort: {priorities}", string.Join(", ", 
            DialogIds.Take(DialogCount).Select(id => DialogsById[id].Priority)));
    }

    public async Task<IDialogData?> TakeDialogAsync() {
        await DialogsSemaphore.WaitAsync();
        try {
            return DialogCount > 0
                ? DialogsById.GetValueOrDefault(DialogIds[0])
                : null;
        }
        finally {
            DialogsSemaphore.Release();   
        }
    }
}
