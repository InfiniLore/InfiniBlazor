// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IEmoteProvider {
    int Count { get; }
    
    bool TryGetEntry(string key,[NotNullWhen(true)] out IEmoteEntry? entry);
    
    Task<bool> TryImportDataAsync(Stream fileStream, CancellationToken ct = default);
    Task<bool> TryWriteDataAsync(StreamWriter streamWriter, CancellationToken ct = default);
}
