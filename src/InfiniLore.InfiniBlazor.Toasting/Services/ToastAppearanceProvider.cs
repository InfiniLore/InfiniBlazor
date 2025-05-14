// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IToastAppearanceProvider>]
public class ToastAppearanceProvider(IToastConfig config) : IToastAppearanceProvider{
    private FrozenDictionary<string, IToastAppearance> StringLookup { get; set; } = config.ToastSetupData.ToFrozenDictionary();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IToastAppearance GetAppearanceOrDefault(object? key) {
        return key switch {
            string s => StringLookup.TryGetValue(s, out IToastAppearance? value) ? value : ToastAppearance.Default,
            _ => ToastAppearance.Default
        };
    }
}
