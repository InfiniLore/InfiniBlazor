// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;

namespace InfiniLore.InfiniBlazor.Toasting.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToastConfig(IInfiniBlazorConfig config) : IToastConfig {
    private readonly Dictionary<string, IToastAppearance> _toastSetupData = new();
    public IReadOnlyDictionary<string, IToastAppearance> ToastSetupData => _toastSetupData.AsReadOnly();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IToastConfig AddToastSetupData(string key, IToastAppearance data) {
        _toastSetupData.AddOrUpdate(key, data);
        return this;
    }
}
