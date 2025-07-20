// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EventCallbackExtensions {
    public static EventCallbackDebouncer<T> GetDebouncer<T>(this EventCallback<T> callback, int debounceMs) => new(callback, debounceMs);
    public static EventCallbackDebouncer GetDebouncer(this EventCallback callback, int debounceMs) => new(callback, debounceMs);
}
