// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DataLoaders;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EmoteProviderFactory {
    public static IEmoteProvider Create(IServiceProvider provider) {
        var dataLoader = provider.GetKeyedService<IEmoteDataLoader>(EmbeddedResourceEmoteDataLoader.KeyName);
        var emoteProvider = ActivatorUtilities.CreateInstance<EmoteProvider>(provider);
        
        if (dataLoader is null) return emoteProvider;
        
        foreach (Stream stream in dataLoader.LoadEmoteStreams()) {
            emoteProvider.TryImportData(stream);
        }

        return emoteProvider;
    }

}
