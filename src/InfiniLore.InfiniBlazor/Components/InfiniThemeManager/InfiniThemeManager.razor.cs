// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.CssData;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniThemeManager(
    IThemeStateProvider themeStateProvider,
    IJsRuntimeHelper jsRuntimeHelper,
    ILogger<InfiniThemeManager> logger,
    IPoolCache pool
) : IAsyncDisposable {
    
    private const string BaseId = "infiniThemeManager-base-css";
    private const string ThemeId = "infiniThemeManager-selected-css";
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnInitializedAsync() {
        themeStateProvider.OnChangedAsync += OnThemeStateChanged;
        
        if (!RendererInfo.IsInteractive) return;

        await OnThemeStateChanged();
    }
    
    private async Task OnThemeStateChanged() {
        IThemeState state = themeStateProvider.GetState();
        
        string? collectionName = await state.TryGetThemeCollectionNameAsync();
        if (collectionName.IsNullOrWhiteSpace()) return;

        IThemeCollection? collection = await themeStateProvider.TryGetCollectionAsync(collectionName);
        if (collection is null) return;

        string? mode = await state.TryGetThemeModeNameAsync();
        if (state.IsNextModeRequested() && collection.TryGetNextThemeMode(mode, out ThemeMode nextMode)) {
            await state.SetThemeModeNameAsync(nextMode.Name);
            mode = nextMode.Name;
        }
        
        if (mode.IsNullOrWhiteSpace()) return;
        if (!collection.TryGetCssData(mode, out ICssData? cssData)) return;
        if (!TryGetCssString(cssData, out string? css)) return;
        
        await jsRuntimeHelper.AddOrUpdateStyleElementAtHead(ThemeId, css);
    }

    public ValueTask DisposeAsync() {
        themeStateProvider.OnChangedAsync -= OnThemeStateChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private bool TryGetCssString(ICssData cssData, [NotNullWhen(true)] out string? css) {
        StringBuilder sb = pool.StringBuilderPool.Get() ;
        try {
            sb.Append(":root{");
            foreach ((string key, string value) in cssData.AsCssVariables()) {
                sb.Append($"{key}:{value};");
            }
            sb.Append('}');
        
            css = sb.ToString();
            return true;
        }
        finally {
            pool.StringBuilderPool.Return(sb);
        }
    }

    private string GetBaseThemeCss() {
        if (TryGetCssString(InfiniBlazorCssData.Instance, out string? css)) return css;

        logger.LogWarning("Could not create base theme CSS.");
        return string.Empty;
    }
}
