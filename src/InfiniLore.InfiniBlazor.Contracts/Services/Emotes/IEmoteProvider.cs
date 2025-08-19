// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Emotes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IEmoteProvider {
    Task InitializeAsync(CancellationToken ct = default);
    
    bool HasKey(string key);
    bool HasKey(ReadOnlySpan<char> key); 
    
    bool TryGetEntry(string key, [NotNullWhen(true)] out IEmoteEntry? entry);
    bool TryGetEntry(ReadOnlySpan<char> span, [NotNullWhen(true)] out IEmoteEntry? entry);
    
    Task<bool> TryImportDataAsync(Stream fileStream, CancellationToken ct = default);
    Task<bool> TryWriteDataAsync(StreamWriter streamWriter, CancellationToken ct = default);
}
