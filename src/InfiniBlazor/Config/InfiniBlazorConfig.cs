// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Emotes.Config;
using InfiniBlazor.Markdown;
using InfiniBlazor.Theming.Config;
using InfiniBlazor.Toasting.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorConfig(IServiceCollection collection) : IInfiniBlazorConfig {
    public IServiceCollection Services { get; } = collection;
    public InfiniBlazorMarkdownConfig Markdown { get; } = new(collection);
    public InfiniBlazorThemingConfig Theming { get; } = new(collection);
    public InfiniBlazorToastingConfig Toasting { get; } = new(collection);
    public InfiniBlazorComponentsConfig Components { get; } = new(collection);
    public InfiniBlazorEmotesConfig Emotes { get; } = new(collection);
}
