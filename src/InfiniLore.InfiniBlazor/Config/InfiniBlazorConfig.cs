// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Theming.Config;
using InfiniLore.InfiniBlazor.Toasting.Config;
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
    public InfiniBlazorToastingConfig Toasting { get; } = new(collection);

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
}
