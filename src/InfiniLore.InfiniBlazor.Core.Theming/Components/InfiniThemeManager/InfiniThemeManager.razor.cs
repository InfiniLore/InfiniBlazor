// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniThemeManager(
    IThemeProvider themeProvider,
    IInfiniBlazorJs infiniBlazorJs,
    IQueryParameterManager queryParameterManager,
    ILogger<InfiniThemeManager> logger
) : ComponentBase, IAsyncDisposable {
    private const string ThemeId = "infiniThemeManager-selected";
    private bool _isUpdatingTheme;
    private string? InitialCss { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    // protected override async Task OnAfterRenderAsync(bool firstRender) {
    //     await OnThemeStateChangedAsync();
    // }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        
        ThemeEntryData? initialTheme = await GetInitialThemeAsync();
        InitialCss = initialTheme?.CssData;
        
        themeProvider.OnThemeChangeRequestAsync += OnThemeChangeRequestAsync;
    }

    private async Task<ThemeEntryData?> GetInitialThemeAsync(CancellationToken ct = default) {
        string? collectionName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeCollection);
        string? entryName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeEntry);
        
        // ReSharper disable once InvertIf
        if (collectionName.IsNotNullOrWhiteSpace() 
            && entryName is not null
            && await themeProvider.GetThemeEntryData(collectionName, entryName, ct) is {} entryData) {
            logger.Information("Found theme entry data in query parameters.");
            return entryData;
        }
        
        return await themeProvider.GetInitialThemeEntyData(ct);
    }
    
    private async Task OnThemeChangeRequestAsync(string collectionName, string entryName) {
        if (_isUpdatingTheme) return;
        _isUpdatingTheme = true;
        
        queryParameterManager.SetParams(
            (QueryParameterNames.ThemeCollection, collectionName),
            (QueryParameterNames.ThemeEntry, entryName)
        );
        
        var ctx = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        try {
            if (await themeProvider.GetThemeEntryData(collectionName, entryName, ctx.Token) is not {} entryData) {
                logger.Warning("Could not find theme entry data.");
                return;
            }

            await infiniBlazorJs.Document.AddOrUpdateElementAtHead(ThemeId, entryData.CssData, ctx.Token);
            InitialCss = null;// Forget the initial state to free up memory
        }
        catch (OperationCanceledException e) {
            logger.Warning(e, "Timeout while updating theme.");
        }
        catch (Exception e) {
            logger.Warning(e, "Error updating theme");
        }
        finally {
            _isUpdatingTheme = false;
        }
    }
    
    public ValueTask DisposeAsync() {
        themeProvider.OnThemeChangeRequestAsync -= OnThemeChangeRequestAsync;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
