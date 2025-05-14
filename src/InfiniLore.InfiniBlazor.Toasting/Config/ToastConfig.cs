// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;

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
        return this;
    }
}
