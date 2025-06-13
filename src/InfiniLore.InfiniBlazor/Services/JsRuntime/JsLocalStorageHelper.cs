// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.JsRuntime;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsLocalStorageHelper>]
public class JsLocalStorageHelper(IJSRuntime jsRuntime, ILogger<JsRuntimeHelper> logger) : IJsLocalStorageHelper {
    
    public class ExpiringStorageItem<T> {
        [JsonPropertyName("data")] public T? Data { get; init; }
        [JsonPropertyName("expiry")] public DateTime? Expiry { get; init; }
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<T?> TryGetValueAsync<T>(string key, CancellationToken ct = default) {
        try {
            string json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", ct, key);
            if (json.IsNullOrEmpty()) return default;

            var item = JsonSerializer.Deserialize<ExpiringStorageItem<T>>(json);
            if (item == null) return default;

            if (!item.Expiry.HasValue || item.Expiry.Value >= DateTime.UtcNow) return item.Data;

            await TryRemoveValueAsync(key, ct); // Optional cleanup
            return default;

        }
        catch (Exception e) {
            logger.Error(e, "Error getting value from local storage for key '{Key}'", key);
            return default;
        }
    }

    public async Task<bool> TrySetValueAsync<T>(string key, T value, TimeSpan? expiresIn = null, CancellationToken ct = default) {
        try {
            ExpiringStorageItem<T> item = new () {
                Data = value,
                Expiry = expiresIn.HasValue ? DateTime.UtcNow.Add(expiresIn.Value) : null
            };

            string json = JsonSerializer.Serialize(item);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", ct, key, json);
            return true;
        }
        catch (Exception e) {
            logger.Error(e, "Error setting value in local storage for key '{Key}'", key);
            return false;
        }
    }

    public async Task<bool> TryRemoveValueAsync(string key, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", ct, key);
            return true;
        }
        catch (Exception e) {
            logger.Error(e, "Error removing value from local storage for key '{Key}'", key);
            return false;
        }
    }
}
