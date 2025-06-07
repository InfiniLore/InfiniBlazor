// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Debugger;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IVisualDebuggerProvider>]
public class VisualDebuggerProvider : IVisualDebuggerProvider {
    private DebuggerState State { get; set; } = DebuggerState.Disabled;
    
    public event Action? OnChange;
    public event Func<Task>? OnChangeAsync;
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
        OnChange?.Invoke();
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
    
    public string GetAsStripes(DebugColor color) 
        => color switch {
            DebugColor.Red => "bg-stripes bg-stripes-(--debug-red)",
            DebugColor.Orange => "bg-stripes bg-stripes-(--debug-orange)",
            DebugColor.Yellow => "bg-stripes bg-stripes-(--debug-yellow)",
            DebugColor.Green => "bg-stripes bg-stripes-(--debug-green)",
            DebugColor.Cyan => "bg-stripes bg-stripes-(--debug-cyan)",
            DebugColor.Blue => "bg-stripes bg-stripes-(--debug-blue)",
            DebugColor.Purple => "bg-stripes bg-stripes-(--debug-purple)",
            DebugColor.Pink => "bg-stripes bg-stripes-(--debug-pink)",
            DebugColor.Gray => "bg-stripes bg-stripes-(--debug-gray)",
            DebugColor.White => "bg-stripes bg-stripes-(--debug-white)",
            DebugColor.Black => "bg-stripes bg-stripes-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorder(DebugColor color)
        => color switch {
            DebugColor.Red => "border border-(--debug-red)",
            DebugColor.Orange => "border border-(--debug-orange)",
            DebugColor.Yellow => "border border-(--debug-yellow)",
            DebugColor.Green => "border border-(--debug-green)",
            DebugColor.Cyan => "border border-(--debug-cyan)",
            DebugColor.Blue => "border border-(--debug-blue)",
            DebugColor.Purple => "border border-(--debug-purple)",
            DebugColor.Pink => "border border-(--debug-pink)",
            DebugColor.Gray => "border border-(--debug-gray)",
            DebugColor.White => "border border-(--debug-white)",
            DebugColor.Black => "border border-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

    public string GetAsBorderTop(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-t border-t-(--debug-red)",
            DebugColor.Orange => "border-t border-t-(--debug-orange)",
            DebugColor.Yellow => "border-t border-t-(--debug-yellow)",
            DebugColor.Green => "border-t border-t-(--debug-green)",
            DebugColor.Cyan => "border-t border-t-(--debug-cyan)",
            DebugColor.Blue => "border-t border-t-(--debug-blue)",
            DebugColor.Purple => "border-t border-t-(--debug-purple)",
            DebugColor.Pink => "border-t border-t-(--debug-pink)",
            DebugColor.Gray => "border-t border-t-(--debug-gray)",
            DebugColor.White => "border-t border-t-(--debug-white)",
            DebugColor.Black => "border-t border-t-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderRight(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-r border-r-(--debug-red)",
            DebugColor.Orange => "border-r border-r-(--debug-orange)",
            DebugColor.Yellow => "border-r border-r-(--debug-yellow)",
            DebugColor.Green => "border-r border-r-(--debug-green)",
            DebugColor.Cyan => "border-r border-r-(--debug-cyan)",
            DebugColor.Blue => "border-r border-r-(--debug-blue)",
            DebugColor.Purple => "border-r border-r-(--debug-purple)",
            DebugColor.Pink => "border-r border-r-(--debug-pink)",
            DebugColor.Gray => "border-r border-r-(--debug-gray)",
            DebugColor.White => "border-r border-r-(--debug-white)",
            DebugColor.Black => "border-r border-r-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderBottom(DebugColor color) 
        => color switch {
            DebugColor.Red => "border-b border-b-(--debug-red)",
            DebugColor.Orange => "border-b border-b-(--debug-orange)",
            DebugColor.Yellow => "border-b border-b-(--debug-yellow)",
            DebugColor.Green => "border-b border-b-(--debug-green)",
            DebugColor.Cyan => "border-b border-b-(--debug-cyan)",
            DebugColor.Blue => "border-b border-b-(--debug-blue)",
            DebugColor.Purple => "border-b border-b-(--debug-purple)",
            DebugColor.Pink => "border-b border-b-(--debug-pink)",
            DebugColor.Gray => "border-b border-b-(--debug-gray)",
            DebugColor.White => "border-b border-b-(--debug-white)",
            DebugColor.Black => "border-b border-b-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    
    public string GetAsBorderLeft(DebugColor color)
        => color switch {
            DebugColor.Red => "border-l border-l-(--debug-red)",
            DebugColor.Orange => "border-l border-l-(--debug-orange)",
            DebugColor.Yellow => "border-l border-l-(--debug-yellow)",
            DebugColor.Green => "border-l border-l-(--debug-green)",
            DebugColor.Cyan => "border-l border-l-(--debug-cyan)",
            DebugColor.Blue => "border-l border-l-(--debug-blue)",
            DebugColor.Purple => "border-l border-l-(--debug-purple)",
            DebugColor.Pink => "border-l border-l-(--debug-pink)",
            DebugColor.Gray => "border-l border-l-(--debug-gray)",
            DebugColor.White => "border-l border-l-(--debug-white)",
            DebugColor.Black => "border-l border-l-(--debug-black)",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
}
