// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Debugger;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IVisualDebuggerProvider>]
public class VisualDebuggerProvider(NavigationManager navigationManager) : IVisualDebuggerProvider {
    private DebuggerState State { get; set; } = DebuggerState.Disabled;
    
    public event Action? OnChange;
    public event Func<Task>? OnChangeAsync;
    
    private const string DebugQueryParam = "infiniblazor-debug";
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool IsEnabled() 
        => State is DebuggerState.Enabled;
    
    public async Task ToggleStateAsync() {
        State = State switch {
            DebuggerState.Disabled => DebuggerState.Enabled,
            DebuggerState.Enabled => DebuggerState.Disabled,
            _ => throw new ArgumentOutOfRangeException()
        };
    
        UpdateDebugQueryParam(State == DebuggerState.Enabled);

        OnChange?.Invoke();
        if (OnChangeAsync is not null) await OnChangeAsync();
    }

    public string AddDebugQueryParam(string uri) 
        => State is DebuggerState.Enabled 
            ? QueryHelpers.AddQueryString(uri, DebugQueryParam, "true")
            : uri;

    private void UpdateDebugQueryParam(bool enabled) {
        Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

        string baseUri = uri.GetLeftPart(UriPartial.Path);
        Dictionary<string, StringValues> query = QueryHelpers.ParseQuery(uri.Query);

        var dict = new Dictionary<string, string?>();
        foreach ((string k, StringValues v) in query) {
            dict[k] = v.ToString();
        }

        if (enabled) dict[DebugQueryParam] = "true";
        else dict.Remove(DebugQueryParam);

        string newUri = QueryHelpers.AddQueryString(baseUri, dict);
        navigationManager.NavigateTo(newUri, forceLoad: false);
    }
    
    public void InitializeFromUrl() {
        Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);

        if (query.TryGetValue(DebugQueryParam, out var val) && bool.TryParse(val.ToString(), out bool enabled) && enabled) {
            State = DebuggerState.Enabled;
        }
        else {
            State = DebuggerState.Disabled;
        }
    }

    public async Task SetStateAsync(DebuggerState state) {
        State = state;
        OnChange?.Invoke();
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
    
    public string GetAsStripes(DebugColor color) 
        => color switch {
            DebugColor.Red => "bg-stripes bg-stripes-(--color-red)",
            DebugColor.Orange => "bg-stripes bg-stripes-(--color-orange)",
            DebugColor.Yellow => "bg-stripes bg-stripes-(--color-yellow)",
            DebugColor.Green => "bg-stripes bg-stripes-(--color-green)",
            DebugColor.Cyan => "bg-stripes bg-stripes-(--color-cyan)",
            DebugColor.Blue => "bg-stripes bg-stripes-(--color-blue)",
            DebugColor.Purple => "bg-stripes bg-stripes-(--color-purple)",
            DebugColor.Pink => "bg-stripes bg-stripes-(--color-pink)",
            DebugColor.Gray => "bg-stripes bg-stripes-(--color-gray)",
            DebugColor.White => "bg-stripes bg-stripes-(--color-white)",
            DebugColor.Black => "bg-stripes bg-stripes-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorder(DebugColor color)
        => color switch {
            DebugColor.Red => "border border-(--color-red)",
            DebugColor.Orange => "border border-(--color-orange)",
            DebugColor.Yellow => "border border-(--color-yellow)",
            DebugColor.Green => "border border-(--color-green)",
            DebugColor.Cyan => "border border-(--color-cyan)",
            DebugColor.Blue => "border border-(--color-blue)",
            DebugColor.Purple => "border border-(--color-purple)",
            DebugColor.Pink => "border border-(--color-pink)",
            DebugColor.Gray => "border border-(--color-gray)",
            DebugColor.White => "border border-(--color-white)",
            DebugColor.Black => "border border-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

    public string GetAsBorderTop(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-t border-t-(--color-red)",
            DebugColor.Orange => "border-t border-t-(--color-orange)",
            DebugColor.Yellow => "border-t border-t-(--color-yellow)",
            DebugColor.Green => "border-t border-t-(--color-green)",
            DebugColor.Cyan => "border-t border-t-(--color-cyan)",
            DebugColor.Blue => "border-t border-t-(--color-blue)",
            DebugColor.Purple => "border-t border-t-(--color-purple)",
            DebugColor.Pink => "border-t border-t-(--color-pink)",
            DebugColor.Gray => "border-t border-t-(--color-gray)",
            DebugColor.White => "border-t border-t-(--color-white)",
            DebugColor.Black => "border-t border-t-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderRight(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-r border-r-(--color-red)",
            DebugColor.Orange => "border-r border-r-(--color-orange)",
            DebugColor.Yellow => "border-r border-r-(--color-yellow)",
            DebugColor.Green => "border-r border-r-(--color-green)",
            DebugColor.Cyan => "border-r border-r-(--color-cyan)",
            DebugColor.Blue => "border-r border-r-(--color-blue)",
            DebugColor.Purple => "border-r border-r-(--color-purple)",
            DebugColor.Pink => "border-r border-r-(--color-pink)",
            DebugColor.Gray => "border-r border-r-(--color-gray)",
            DebugColor.White => "border-r border-r-(--color-white)",
            DebugColor.Black => "border-r border-r-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderBottom(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-b border-b-(--color-red)",
            DebugColor.Orange => "border-b border-b-(--color-orange)",
            DebugColor.Yellow => "border-b border-b-(--color-yellow)",
            DebugColor.Green => "border-b border-b-(--color-green)",
            DebugColor.Cyan => "border-b border-b-(--color-cyan)",
            DebugColor.Blue => "border-b border-b-(--color-blue)",
            DebugColor.Purple => "border-b border-b-(--color-purple)",
            DebugColor.Pink => "border-b border-b-(--color-pink)",
            DebugColor.Gray => "border-b border-b-(--color-gray)",
            DebugColor.White => "border-b border-b-(--color-white)",
            DebugColor.Black => "border-b border-b-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderLeft(DebugColor color)
        => color switch {
            DebugColor.Red => "border-l border-l-(--color-red)",
            DebugColor.Orange => "border-l border-l-(--color-orange)",
            DebugColor.Yellow => "border-l border-l-(--color-yellow)",
            DebugColor.Green => "border-l border-l-(--color-green)",
            DebugColor.Cyan => "border-l border-l-(--color-cyan)",
            DebugColor.Blue => "border-l border-l-(--color-blue)",
            DebugColor.Purple => "border-l border-l-(--color-purple)",
            DebugColor.Pink => "border-l border-l-(--color-pink)",
            DebugColor.Gray => "border-l border-l-(--color-gray)",
            DebugColor.White => "border-l border-l-(--color-white)",
            DebugColor.Black => "border-l border-l-(--color-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string? WithEnabled(string? onTrue, string? onFalse) 
        => State switch {
            DebuggerState.Enabled => onTrue,
            DebuggerState.Disabled => onFalse,
            _ => throw new ArgumentOutOfRangeException()
        };
}
