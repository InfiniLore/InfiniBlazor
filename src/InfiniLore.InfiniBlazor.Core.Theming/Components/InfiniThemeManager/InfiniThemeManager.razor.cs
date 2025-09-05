// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Pooling;
using InfiniLore.InfiniBlazor.Theming.CssData;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniThemeManager(
    IThemeStateProvider themeStateProvider,
    IJsInfiniBlazor jsInfiniBlazor,
    ILogger<InfiniThemeManager> logger
) : ComponentBase, IAsyncDisposable {

    private const string BaseId = "infiniThemeManager-base";
    private const string ThemeId = "infiniThemeManager-selected";
    private bool _isUpdatingTheme;
    private string? InitialCss { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await OnThemeStateChanged();

        if (firstRender) {
            themeStateProvider.OnChangedAsync += OnThemeStateChanged;
            
            // Fixes and issue when used in MAUI BlazorHybrid as there is not good HeadContent operations
            // just need to add <style id="infiniThemeManager-base-css"></style> at the desired location in the index.html of a MAUI app
            await jsInfiniBlazor.Document.AddOrUpdateElementAtHead(BaseId, GetBaseThemeCss());
        }
    }

    protected override async Task OnInitializedAsync() {
        await themeStateProvider.InitializeFromQueryAsync();
        await PreloadInitialThemeCss();
    }

    private async Task OnThemeStateChanged() {
        if (_isUpdatingTheme) return;

        _isUpdatingTheme = true;
        try {
            IThemeState state = themeStateProvider.GetState();

            string? collectionName = state.CollectionName;
            if (collectionName.IsNullOrWhiteSpace()) {
                logger.Warning("Could not find theme collection.");
                return;
            }

            IThemeCollection? collection = await themeStateProvider.TryGetCollectionAsync(collectionName);
            if (collection is null) {
                logger.Warning("Could not find theme collection.");
                return;
            }

            string? mode = state.ModeName;
            if (mode.IsNullOrWhiteSpace()) {
                mode = collection.GetFirstMode().Name;
            }

            if (!collection.TryGetCssData(mode, out ICssData? cssData)) {
                logger.Warning("Could not find theme mode CSS data.");
                return;
            }

            if (!TryGetCssString(cssData, out string? css)) {
                logger.Warning("Could not create theme CSS.");
                return;
            }

            await jsInfiniBlazor.Document.AddOrUpdateElementAtHead(ThemeId, css);
            InitialCss = null; // Forget the initial state to free up memory
        }
        finally {
            _isUpdatingTheme = false;
        }
    }


    private async Task PreloadInitialThemeCss() {
        try {
            IThemeState state = themeStateProvider.GetState();

            string? collectionName = state.CollectionName;
            if (collectionName.IsNullOrWhiteSpace()) return;

            IThemeCollection? collection = await themeStateProvider.TryGetCollectionAsync(collectionName);
            if (collection is null) return;

            string? mode = state.ModeName;
            if (mode.IsNullOrWhiteSpace()) {
                mode = collection.GetFirstMode().Name;
            }

            if (collection.TryGetCssData(mode, out ICssData? cssData) &&
                TryGetCssString(cssData, out string? css)) {
                InitialCss = css;
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "Failed to preload theme CSS.");
        }
    }
    
    public ValueTask DisposeAsync() {
        themeStateProvider.OnChangedAsync -= OnThemeStateChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private static bool TryGetCssString(ICssData cssData, [NotNullWhen(true)] out string? css) {
        StringBuilder sb = GlobalPools.StringBuilder.Get();
        try {
            sb.Append(":root{");
            foreach ((string key, string value) in cssData.AsCssVariables()) {
                sb.Append(key);
                sb.Append(':');
                sb.Append(value);
                sb.Append(';');
            }
            sb.Append('}');

            css = sb.ToString();
            return true;
        }
        finally {
            GlobalPools.StringBuilder.Return(sb);
        }
    }

    private string GetBaseThemeCss() {
        if (TryGetCssString(InfiniBlazorCssData.Instance, out string? css)) return css;
        logger.Warning("Could not create base theme CSS.");
        return ":root{}";
    }
}
