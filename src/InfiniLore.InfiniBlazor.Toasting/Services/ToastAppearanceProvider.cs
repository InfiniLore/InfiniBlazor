// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IToastAppearanceProvider>]
public class ToastAppearanceProvider(IToastConfig config) : IToastAppearanceProvider{
    private readonly FrozenDictionary<string, IToastAppearance> StringLookup = config.ToastSetupData
        .Where(pair => pair.Key is string)
        .ToFrozenDictionary(pair => (pair.Key as string)!, pair => pair.Value);
    
    private readonly FrozenDictionary<StandardToastAppearance, IToastAppearance> StandardLookup = config.ToastSetupData
        .Where(pair => pair.Key is StandardToastAppearance)
        .ToFrozenDictionary(pair => (StandardToastAppearance)pair.Key, pair => pair.Value);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IToastAppearance GetAppearanceOrDefault(object? key) {
        return key switch {
            string s => StringLookup.TryGetValue(s, out IToastAppearance? value) ? value : ToastAppearance.Default,
            StandardToastAppearance d => StandardLookup.TryGetValue(d, out IToastAppearance? value) ? value : ToastAppearance.Default,
            _ => ToastAppearance.Default
        };
    }
}
