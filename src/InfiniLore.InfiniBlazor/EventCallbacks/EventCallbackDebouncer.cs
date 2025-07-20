// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EventCallbackDebouncer(EventCallback callback, int debounceMs = EventCallbackDebouncer.DefaultDebounceMs)
    : IAsyncDisposable {

    private const int DefaultDebounceMs = 100;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private CancellationTokenSource? _cts;
    private Task? _debounceTask;
    private bool _isDisposed;

    public async Task InvokeDebouncedAsync() {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        await _semaphore.WaitAsync();
        try {
            if (_cts is not null) await _cts.CancelAsync();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            CancellationTokenSource? localCts = _cts;

            _debounceTask = Task.Run(function: async () => {
                try {
                    await Task.Delay(debounceMs, localCts.Token);
                    if (!localCts.Token.IsCancellationRequested && callback.HasDelegate) {
                        await callback.InvokeAsync();
                    }
                }
                catch (OperationCanceledException) {
                    // Ignore
                }
            }, localCts.Token);
        }
        finally {
            _semaphore.Release();
        }
    }

    public async ValueTask DisposeAsync() {
        if (_isDisposed) return;

        _isDisposed = true;

        await _semaphore.WaitAsync();
        try {
            _cts?.Cancel();
            _cts?.Dispose();
            if (_debounceTask is not null) {
                try { await _debounceTask; }
                catch {
                    /* Ignore */
                }
            }
        }
        finally {
            _semaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
