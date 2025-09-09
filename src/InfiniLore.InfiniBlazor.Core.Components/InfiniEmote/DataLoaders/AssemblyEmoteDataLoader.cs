// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace InfiniLore.InfiniBlazor.Components.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AssemblyEmoteDataLoader(Assembly assembly, IEnumerable<string> resourceNames, ILogger<AssemblyEmoteDataLoader> logger) : IEmoteDataLoader {
    public bool EnforceAsyncUsage => false;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task<Stream[]> LoadEmoteStreamsAsync(CancellationToken ct = default) {
        throw new NotSupportedException();
    }
    
    public IEnumerable<Stream> LoadEmoteStreams() {
        foreach (string resourceName in resourceNames) {
            Stream? stream = null;
            try {
                stream = assembly.GetManifestResourceStream(resourceName);
            }
            catch (Exception ex) {
                logger.Warning(ex, "Failed to load embedded resource: {resourceName}", resourceName);
            }
            
            if (stream is not null) yield return stream;
            else logger.Warning("Could not find embedded resource: {resourceName}", resourceName);
        }

    }
}
