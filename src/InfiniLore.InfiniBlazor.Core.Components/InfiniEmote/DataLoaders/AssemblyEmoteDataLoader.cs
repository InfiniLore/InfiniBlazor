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

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task<Stream[]> LoadEmoteStreamsAsync(CancellationToken ct = default) {
        var streams = new List<Stream>();
        foreach (string resourceName in resourceNames) {
            try {
                Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null) {
                    streams.Add(stream);
                }
                else {
                    logger.Warning("Could not find embedded resource: {resourceName}", resourceName);
                }
            }
            catch (Exception ex) {
                logger.Warning(ex, "Failed to load embedded resource: {resourceName}", resourceName);
            }
        }

        return Task.FromResult(streams.ToArray());
    }
}
