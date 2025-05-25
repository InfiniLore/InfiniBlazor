// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Runtime.InteropServices;

namespace Infinilore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[StructLayout(LayoutKind.Explicit, Size = 25)]
public readonly record struct OnClickUnion {
    [field: FieldOffset(0)] private byte State { get; init; }
    [field: FieldOffset(8)] private EventCallback<MouseEventArgs> OnClick { get; init; }
    [field: FieldOffset(8)] private Action? OnClickAction { get; init; }
    [field: FieldOffset(8)] private Action<MouseEventArgs>? OnClickActionWithArgs { get; init; }
    [field: FieldOffset(8)] private Func<Task>? OnClickAsync { get; init; }
    [field: FieldOffset(8)] private Func<MouseEventArgs, Task>? OnClickAsyncWithArgs { get; init; }

    // -----------------------------------------------------------------------------------------------------------------
    // Implicit operators
    // -----------------------------------------------------------------------------------------------------------------    
    public static implicit operator OnClickUnion(EventCallback<MouseEventArgs> onClick) => new() { OnClick = onClick, State = 1 };
    public static implicit operator OnClickUnion(Action onClick) => new() { OnClickAction = onClick, State = 2 };
    public static implicit operator OnClickUnion(Action<MouseEventArgs> onClick) => new() { OnClickActionWithArgs = onClick, State = 3 };
    public static implicit operator OnClickUnion(Func<Task> onClick) => new() { OnClickAsync = onClick, State = 4 };
    public static implicit operator OnClickUnion(Func<MouseEventArgs, Task> onClick) => new() { OnClickAsyncWithArgs = onClick, State = 5 };
    
    public static implicit operator OnClickUnion(Delegate onClick) => onClick switch {
        Action action => new OnClickUnion { OnClickAction = action, State = 2 },
        Action<MouseEventArgs> actionWithArgs => new OnClickUnion { OnClickActionWithArgs = actionWithArgs, State = 3 },
        Func<Task> asyncAction => new OnClickUnion { OnClickAsync = asyncAction, State = 4 },
        Func<MouseEventArgs, Task> asyncActionWithArgs => new OnClickUnion { OnClickAsyncWithArgs = asyncActionWithArgs, State = 5 },
        _ => throw new ArgumentException($"Unsupported delegate type: {onClick.GetType()}", nameof(onClick))
    };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InvokeAsync(MouseEventArgs args) {
        switch (State) {
            case 0:
                break;
            case 1:
                await OnClick.InvokeAsync(args);
                break;
            case 2:
                OnClickAction?.Invoke();
                break;
            case 3:
                OnClickActionWithArgs?.Invoke(args);
                break;
            case 4:
                await OnClickAsync?.Invoke()!;
                break;
            case 5:
                await OnClickAsyncWithArgs?.Invoke(args)!;
                break;
        }
    }
}
