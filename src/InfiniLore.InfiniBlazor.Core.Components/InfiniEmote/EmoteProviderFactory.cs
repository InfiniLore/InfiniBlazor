// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DataLoaders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EmoteProviderFactory {
    public static IEmoteProvider Create(IServiceProvider provider) {
        var dataLoader = provider.GetKeyedService<IEmoteDataLoader>(EmbeddedResourceEmoteDataLoader.KeyName);
        var emoteProvider = ActivatorUtilities.CreateInstance<EmoteProvider>(provider);
        var logger = provider.GetRequiredService<ILogger<EmoteProvider>>();
        
        if (dataLoader is null) return emoteProvider;
        if (dataLoader.EnforceAsyncUsage) {
            logger.Warning("Emote data loader {loaderName} does not support async usage. Use synchronous loader instead.", dataLoader.GetType().Name);
            return emoteProvider;
        }
        
        foreach (Stream stream in dataLoader.LoadEmoteStreams()) {
            emoteProvider.TryImportData(stream);
        }

        return emoteProvider;
    }

}
