// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Theming;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniThemeManager(IThemeSelector themeSelector, IJsRuntimeHelper jsRuntimeHelper) : IAsyncDisposable {
    private string CurrentCss { get; set; } = string.Empty;
    private const string StyleId = "infiniThemeManager-selectedTheme-css";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        themeSelector.ThemeChangedAsync += OnThemeChangedAsync;
        if (!themeSelector.TryGetCurrentThemeModeCss(out string? css)) return;

        CurrentCss = css;
    }
    
    private async Task OnThemeChangedAsync() {
        if (!themeSelector.TryGetCurrentThemeModeCss(out string? css)) return;

        CurrentCss = css;

        // WTF this works ...
        //      HeadContent should have handled this, but NO it doesn't want to do it ... why? I don't know!
        await jsRuntimeHelper.AddOrUpdateStyleElementAtHead(StyleId, CurrentCss);
        await InvokeAsync(StateHasChanged);
    }
    
    public ValueTask DisposeAsync() {
        themeSelector.ThemeChangedAsync -= OnThemeChangedAsync;
        return ValueTask.CompletedTask;
    }
}
