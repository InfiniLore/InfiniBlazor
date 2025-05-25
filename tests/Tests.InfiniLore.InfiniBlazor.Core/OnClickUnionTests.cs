// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Infinilore.InfiniBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Tests.InfiniLore.InfiniBlazor.Core;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class OnClickUnionTests {
    private static byte GetState(OnClickUnion union) {
        PropertyInfo? stateField = typeof(OnClickUnion).GetProperty("State", BindingFlags.NonPublic | BindingFlags.Instance);
        byte actualState = (byte)stateField!.GetValue(union)!;
        return actualState;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Tests
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    public async Task DefaultState_ShouldBeZero() {
        // Arrange
        var union = new OnClickUnion();

        // Act
        byte actualState = GetState(union);

        // Assert
        await Assert.That(actualState).IsZero();
    }

    [Test]
    public async Task EventCallback_ShouldInvoke() {
        bool called = false;
        var args = new MouseEventArgs();

        EventCallback<MouseEventArgs> callback = EventCallback.Factory.Create<MouseEventArgs>(this, _ => called = true);
        OnClickUnion union = callback;

        await union.InvokeAsync(args);
        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task Action_ShouldInvoke() {
        bool called = false;
        OnClickUnion union = (Action)Action;

        await union.InvokeAsync(new MouseEventArgs());
        await Assert.That(called).IsTrue();
        return;

        void Action() => called = true;
    }

    [Test]
    public async Task ActionWithArgs_ShouldInvoke() {
        MouseEventArgs? receivedArgs = null;
        var args = new MouseEventArgs();

        OnClickUnion union = (Action<MouseEventArgs>)Action;

        await union.InvokeAsync(args);
        await Assert.That(receivedArgs).IsEqualTo(args);
        return;

        void Action(MouseEventArgs e) => receivedArgs = e;
    }

    [Test]
    public async Task AsyncFunc_ShouldInvoke() {
        bool called = false;
        Func<Task> action = () => {
            called = true;
            return Task.CompletedTask;
        };
        OnClickUnion union = action;

        await union.InvokeAsync(new MouseEventArgs());
        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task AsyncFuncWithArgs_ShouldInvoke() {
        MouseEventArgs? receivedArgs = null;
        var args = new MouseEventArgs();

        Func<MouseEventArgs, Task> action = e => {
            receivedArgs = e;
            return Task.CompletedTask;
        };
        OnClickUnion union = action;

        await union.InvokeAsync(args);
        await Assert.That(receivedArgs).IsEqualTo(args);
    }

    [Test]
    public async Task NullDelegates_ShouldNotThrow() {
        OnClickUnion union = new();
        await union.InvokeAsync(new MouseEventArgs());
        // Test passes if no exception is thrown
    }

    [Test]
    [Arguments(1)]// EventCallback
    [Arguments(2)]// Action
    [Arguments(3)]// Action<MouseEventArgs>
    [Arguments(4)]// Func<Task>
    [Arguments(5)]// Func<MouseEventArgs, Task>
    public async Task State_ShouldBeCorrect(byte expectedState) {
        OnClickUnion union = expectedState switch {
            1 => EventCallback.Factory.Create<MouseEventArgs>(this, _ => {}),
            2 => new Action(() => {}),
            3 => new Action<MouseEventArgs>(_ => {}),
            4 => new Func<Task>(() => Task.CompletedTask),
            5 => new Func<MouseEventArgs, Task>(_ => Task.CompletedTask),
            _ => throw new ArgumentException("Invalid state")
        };

        // Use reflection to access private State field
        PropertyInfo? stateField = typeof(OnClickUnion).GetProperty("State", BindingFlags.NonPublic | BindingFlags.Instance);
        byte actualState = (byte)stateField!.GetValue(union)!;

        await Assert.That(actualState).IsEqualTo(expectedState);
    }
}
