// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Theming;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniThemeManager(IThemeSelector themeSelector, IJsRuntimeHelper jsRuntimeHelper, ILogger<InfiniThemeManager> logger) : IAsyncDisposable {
    private string CurrentCss { get; set; } = string.Empty;
    private const string BaseId = "infiniThemeManager-base-css";
    private const string ThemeId = "infiniThemeManager-selectedTheme-css";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        themeSelector.ThemeChangedAsync += OnThemeChangedAsync;
        if (!themeSelector.TryGetCurrentThemeModeCss(out string? css)) return;

        CurrentCss = css;
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender) return;
        await OnThemeChangedAsync();
    }

    private async Task OnThemeChangedAsync() {
        if (!themeSelector.TryGetCurrentThemeModeCss(out string? css)) return;

        CurrentCss = css;

        // WTF this works ...
        //      HeadContent should have handled this, but NO it doesn't want to do it ... why? I don't know!
        await jsRuntimeHelper.AddOrUpdateStyleElementAtHead(ThemeId, CurrentCss);
        await InvokeAsync(StateHasChanged);
    }

    public ValueTask DisposeAsync() {
        themeSelector.ThemeChangedAsync -= OnThemeChangedAsync;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private string GetBaseThemeCss() {
        if (themeSelector.TryCreateCssString(InfiniBlazorTheme.Instance, out string? css)) return css;

        logger.LogWarning("Could not create base theme CSS.");
        return string.Empty;
    }
}
