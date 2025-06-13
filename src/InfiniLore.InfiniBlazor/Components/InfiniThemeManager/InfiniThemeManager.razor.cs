// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Collections;
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
    IThemeProvider themeProvider,
    IJsRuntimeHelper jsRuntimeHelper,
    ILogger<InfiniThemeManager> logger,
    IThemingConfig themingConfig,
    IPoolCache pool
) : IAsyncDisposable {
    private const string BaseId = "infiniThemeManager-base-css";
    private const string ThemeId = "infiniThemeManager-selected-css";
    
    private IThemeCollection CurrentThemeCollection { get; set; } = themingConfig.RegisteredBaseThemes.GetValueOrDefault(themingConfig.DefaultThemeCollectionName) ?? new DefaultThemeCollection();
    private string CurrentModeName { get; set; } = themingConfig.DefaultThemeMode.Name;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        themeProvider.ThemeChangedAsync += OnThemeChangedAsync;
        themeProvider.ModeChangedAsync += OnModeChangedAsync;
    }

    private async Task OnThemeChangedAsync(IThemeCollection? theme = null) {
        CurrentThemeCollection = theme ?? CurrentThemeCollection;
        await UpdateCss();
    }
    
    private async Task OnModeChangedAsync(string? modeName = null) {
        if (!CurrentThemeCollection.TryGetNextThemeMode(modeName ?? CurrentModeName, out ThemeMode mode)) return;
        CurrentModeName = mode.Name;
        await UpdateCss();
    }

    private async ValueTask UpdateCss() {
        if (!CurrentThemeCollection.TryGetCssData(CurrentModeName, out ICssData? cssData)) {
            CurrentThemeCollection.TryGetNextThemeMode(null, out ThemeMode mode);
            CurrentModeName = mode.Name;
            if (!CurrentThemeCollection.TryGetCssData(CurrentModeName, out cssData))return;
        }
        if (!TryGetCssString(cssData, out string? css)) return;

        await jsRuntimeHelper.AddOrUpdateStyleElementAtHead(ThemeId, css);
    }

    public ValueTask DisposeAsync() {
        themeProvider.ThemeChangedAsync -= OnThemeChangedAsync;
        themeProvider.ModeChangedAsync -= OnModeChangedAsync;
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
    
    private string? GetCurrentModeCss() {
        if (!CurrentThemeCollection.TryGetCssData(CurrentModeName, out ICssData? cssData)) return null;
        TryGetCssString(cssData, out string? css);
        return css;
    }
}
