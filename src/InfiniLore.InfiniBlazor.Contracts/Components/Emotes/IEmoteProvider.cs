// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IEmoteProvider {
    int Count { get; }
    
    Task InitializeAsync(CancellationToken ct = default);
    
    IEmoteEntry? GetEntryAsync(string key);
    
    Task<bool> TryImportDataAsync(Stream fileStream, CancellationToken ct = default);
    Task<bool> TryWriteDataAsync(StreamWriter streamWriter, CancellationToken ct = default);
}
