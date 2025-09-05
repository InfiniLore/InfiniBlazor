// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.ToastAppearances;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Theming.Config;
using InfiniLore.InfiniBlazor.Toasting;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorConfig(IServiceCollection collection) : IInfiniBlazorConfig {
    public IServiceCollection Services { get; } = collection;
    public InfiniBlazorMarkdownConfig Markdown { get; } = new(collection);
    public InfiniBlazorThemingConfig Theming { get; } = new(collection);


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

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorConfig SetComponentRenderMode(IComponentRenderMode renderMode) {
        RenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }

    public InfiniBlazorConfig RegisterToastAppearance<TComponent>(string appearanceName) where TComponent : ToastMessageBase {
        ToastAppearanceComponentMappings[appearanceName] = typeof(TComponent);
        return this;
    }
}
