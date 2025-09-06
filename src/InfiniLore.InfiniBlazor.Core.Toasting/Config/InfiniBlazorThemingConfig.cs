// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Core.Toasting;
using InfiniLore.InfiniBlazor.Toasting.ToastAppearances;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Toasting.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorToastingConfig : IToastingConfig {
    public int AutoRemoveDuration { get; set; } = 5000;
    private Dictionary<string, Type> ToastAppearanceComponentMappings { get; } = new() {
        [ToastAppearance.Default.ToName()] = typeof(ToastMessageBase),
        [ToastAppearance.Info.ToName()] = typeof(InfoToastMessage),
        [ToastAppearance.Success.ToName()] = typeof(SuccessToastMessage),
        [ToastAppearance.Warning.ToName()] = typeof(WarningToastMessage),
        [ToastAppearance.Error.ToName()] = typeof(ErrorToastMessage),
        [ToastAppearance.Debug.ToName()] = typeof(DebugToastMessage),
        [ToastAppearance.Achievement.ToName()] = typeof(AchievementToastMessage)
    };

    private Lazy<FrozenDictionary<string, Type>> AppearanceComponentMappingsLazy { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorToastingConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreToasting();
        serviceCollection.AddSingleton<IToastingConfig>(this);
        AppearanceComponentMappingsLazy = new Lazy<FrozenDictionary<string, Type>>(() => ToastAppearanceComponentMappings.ToFrozenDictionary(
        ));
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorToastingConfig RegisterToastAppearance<TComponent>(string appearanceName) where TComponent : ToastMessageBase {
        ToastAppearanceComponentMappings[appearanceName] = typeof(TComponent);
        return this;
    }
    
    public FrozenDictionary<string, Type> GetAppearanceComponentMapping() => AppearanceComponentMappingsLazy.Value;
}
