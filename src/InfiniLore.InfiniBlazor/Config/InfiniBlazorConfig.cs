// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.ToastAppearances;
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Collections;
using InfiniLore.InfiniBlazor.Toasting;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorConfig(IServiceCollection collection) {
    public IServiceCollection Services { get; } = collection;

    internal Dictionary<string, IThemeCollection> RegisteredBaseThemeCollections { get; } = new();
    internal ThemeMode DefaultThemeMode { get; private set; } = ThemeMode.DarkMode;
    internal string DefaultThemeCollectionName { get; private set; } = DefaultThemeCollection.Name;
    
    public int ToastDefaultDuration { get; set; } = 5000;
    internal Dictionary<string, Type> ToastAppearanceComponentMappings { get; } = new() {
        [ToastAppearance.Default.ToName()] = typeof(ToastMessageBase),
        [ToastAppearance.Info.ToName()] = typeof(InfoToastMessage),
        [ToastAppearance.Success.ToName()] = typeof(SuccessToastMessage),
        [ToastAppearance.Warning.ToName()] = typeof(WarningToastMessage),
        [ToastAppearance.Error.ToName()] = typeof(ErrorToastMessage),
        [ToastAppearance.Debug.ToName()] = typeof(DebugToastMessage),
        [ToastAppearance.Achievement.ToName()] = typeof(AchievementToastMessage)
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorConfig SetRenderMode(IComponentRenderMode renderMode) {
        RenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }

    public InfiniBlazorConfig SetRenderModeForMauiBlazorHybrid() {
        // For MAUI this has to be set to null;
        RenderModeProvider.InfiniRenderMode = null!;
        return this;
    }
    
    public InfiniBlazorConfig RegisterTheme<TTheme>() where TTheme : class, IThemeCollection, new() {
        var theme = new TTheme();
        RegisteredBaseThemeCollections.AddOrUpdate(theme.CollectionName, theme);
        return this;
    }

    public InfiniBlazorConfig SetDefaultThemeMode(ThemeMode mode) {
        if (mode.Variant == ThemeModeVariants.Undefined) return this;

        DefaultThemeMode = mode;
        return this;
    }

    public InfiniBlazorConfig SetDefaultThemeCollectionName(string modeName) {
        if (modeName.IsNullOrWhiteSpace()) return this;

        DefaultThemeCollectionName = modeName;
        return this;
    }

    public InfiniBlazorConfig RegisterExternalThemeCollectionProvider<TProvider>() where TProvider : class, IExternalThemeCollectionProvider {
        Services.AddScoped<IExternalThemeCollectionProvider, TProvider>();
        return this;
    }

    public InfiniBlazorConfig RegisterToastAppearance<TComponent>(string appearanceName) where TComponent : ToastMessageBase {
        ToastAppearanceComponentMappings[appearanceName] = typeof(TComponent);
        return this;
    }
}
