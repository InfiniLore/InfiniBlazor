// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Infinilore.InfiniBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TUnit.Assertions.AssertConditions.Throws;

namespace Tests.InfiniLore.InfiniBlazor.Core;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "ConvertToLocalFunction")]
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
        // Arrange
        bool called = false;
        var args = new MouseEventArgs();
        EventCallback<MouseEventArgs> callback = EventCallback.Factory.Create<MouseEventArgs>(this, _ => called = true);
        OnClickUnion union = callback;

        // Act
        await union.InvokeAsync(args);

        // Assert
        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task Action_ShouldInvoke() {
        // Arrange
        bool called = false;
        OnClickUnion union = new Action(() => called = true);
        
        // Act
        await union.InvokeAsync(new MouseEventArgs());

        // Assert
        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task ActionWithArgs_ShouldInvoke() {
        // Arrange
        MouseEventArgs? receivedArgs = null;
        var args = new MouseEventArgs();
        OnClickUnion union = new Action<MouseEventArgs>(e => receivedArgs = e);
        
        // Act
        await union.InvokeAsync(args);

        // Assert
        await Assert.That(receivedArgs).IsEqualTo(args);
    }

    [Test]
    public async Task AsyncFunc_ShouldInvoke() {
        // Arrange
        bool called = false;
        Func<Task> action = () => {
            called = true;
            return Task.CompletedTask;
        };
        OnClickUnion union = action;

        // Act
        await union.InvokeAsync(new MouseEventArgs());

        // Assert
        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task AsyncFuncWithArgs_ShouldInvoke() {
        // Arrange
        MouseEventArgs? receivedArgs = null;
        var args = new MouseEventArgs();
        Func<MouseEventArgs, Task> action = e => {
            receivedArgs = e;
            return Task.CompletedTask;
        };
        OnClickUnion union = action;

        // Act
        await union.InvokeAsync(args);

        // Assert
        await Assert.That(receivedArgs).IsEqualTo(args);
    }

    [Test]
    public async Task NullDelegates_ShouldNotThrow() {
        // Arrange
        OnClickUnion union = new();
        var args = new MouseEventArgs();

        // Act & Assert
        await Assert.That(async () => await union.InvokeAsync(args)).ThrowsNothing();
    }

    [Test]
    [Arguments(1)]// EventCallback
    [Arguments(2)]// Action
    [Arguments(3)]// Action<MouseEventArgs>
    [Arguments(4)]// Func<Task>
    [Arguments(5)]// Func<MouseEventArgs, Task>
    public async Task State_ShouldBeCorrect(byte expectedState) {
        // Arrange
        OnClickUnion union = expectedState switch {
            1 => EventCallback.Factory.Create<MouseEventArgs>(this, _ => {}),
            2 => new Action(() => {}),
            3 => new Action<MouseEventArgs>(_ => {}),
            4 => new Func<Task>(() => Task.CompletedTask),
            5 => new Func<MouseEventArgs, Task>(_ => Task.CompletedTask),
            _ => throw new ArgumentException("Invalid state")
        };

        // Act
        byte actualState = GetState(union);

        // Assert
        await Assert.That(actualState).IsEqualTo(expectedState);
    }
}
