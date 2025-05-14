// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Toasting.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToastConfig(IInfiniBlazorConfig config) : IToastConfig {
    private readonly Dictionary<object, IToastAppearance> _toastSetupData = new();
    public IReadOnlyDictionary<object, IToastAppearance> ToastSetupData => _toastSetupData.AsReadOnly();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IToastConfig AddToastSetupData(object key, IToastAppearance data) {
        _toastSetupData.AddOrUpdate(key, data);
        config.Services.AddKeyedSingleton(key, data);
        return this;
    }
}
