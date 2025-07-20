// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EventCallbackDebouncer<T>(EventCallback<T> callback, int debounceMs = EventCallbackDebouncer<T>.DefaultDebounceMs)
    : IAsyncDisposable {

    private const int DefaultDebounceMs = 100;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private CancellationTokenSource? _cts;
    private Task? _debounceTask;
    private bool _isDisposed;
    private T? _latestValue;

    public async Task InvokeDebouncedAsync(T? value = default) {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        await _semaphore.WaitAsync();
        try {
            _latestValue = value;

            if (_cts is not null) {
                await _cts.CancelAsync();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            _debounceTask = Task.Run(function: async () => {
                try {
                    await Task.Delay(debounceMs, token);
                    if (!token.IsCancellationRequested && callback.HasDelegate) {
                        await callback.InvokeAsync(_latestValue);
                    }
                }
                catch (OperationCanceledException) {
                    // Ignore
                }
            }, token);
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
            if (_cts is not null) {
                await _cts.CancelAsync();
                _cts.Dispose();
            }

            if (_debounceTask is not null) {
                try { await _debounceTask; }
                catch {
                    // Ignore
                }
            }
        }
        finally {
            _semaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
