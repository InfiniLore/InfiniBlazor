// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EventCallbackExtensions {
    public static EventCallbackDebouncer<T> GetDebouncer<T>(this EventCallback<T> callback, int debounceMs) => new(callback, debounceMs);
    public static EventCallbackDebouncer GetDebouncer(this EventCallback callback, int debounceMs) => new(callback, debounceMs);
}

public class EventCallbackDebouncer<T>(EventCallback<T> callback, int debounceMs = EventCallbackDebouncer<T>.DefaultDebounceMs) : IAsyncDisposable {
    private readonly SemaphoreSlim _semaphore = new(1);
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isDisposed;
    private const int DefaultDebounceMs = 100;

    public async Task InvokeDebouncedAsync(T? value = default) {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        if (_isDisposed) return;
        
        await _semaphore.WaitAsync();
        try {

            // Cancel any pending execution
            if (_cancellationTokenSource is not null) {
                await _cancellationTokenSource.CancelAsync();
                _cancellationTokenSource.Dispose();
            }

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = _cancellationTokenSource.Token;

            try {
                await Task.Delay(debounceMs, ct);

                if (!ct.IsCancellationRequested && !_isDisposed) {
                    await callback.InvokeAsync(value);
                }
            }
            catch (OperationCanceledException) {
                // Ignore cancellation
            }
        }
        finally {
            if (!_isDisposed) _semaphore.Release();
        }
    }

    public async ValueTask DisposeAsync() {
        if (_isDisposed) return;
        _isDisposed = true;

        if (_cancellationTokenSource is not null) {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        _semaphore.Dispose();
        GC.SuppressFinalize(this);
    }
}

public class EventCallbackDebouncer(EventCallback callback, int debounceMs = EventCallbackDebouncer.DefaultDebounceMs) : IAsyncDisposable {
    private readonly SemaphoreSlim _semaphore = new(1);
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isDisposed;
    private const int DefaultDebounceMs = 100;

    public async Task InvokeDebouncedAsync() {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        if (_isDisposed) return;

        await _semaphore.WaitAsync();
        try {
            // Cancel any pending execution
            if (_cancellationTokenSource is not null) {
                await _cancellationTokenSource.CancelAsync();
                _cancellationTokenSource.Dispose();
            }

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = _cancellationTokenSource.Token;

            try {
                await Task.Delay(debounceMs, ct);

                if (!ct.IsCancellationRequested && !_isDisposed) {
                    await callback.InvokeAsync();
                }
            }
            catch (OperationCanceledException) {
                // Ignore cancellation
            }
        }
        finally {
            if (!_isDisposed) _semaphore.Release();
        }
    }

    public async ValueTask DisposeAsync() {
        if (_isDisposed) return;
        _isDisposed = true;

        if (_cancellationTokenSource is not null) {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        _semaphore.Dispose();
        GC.SuppressFinalize(this);
    }
}
