// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace Tests.InfiniLore.InfiniBlazor.EventCallbacks;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EventCallbackDebouncerTests {

    [Test]
    public async Task DefaultDebounceMs_ShouldBe100() {
        // Arrange
        int callCount = 0;
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => {
            callCount++;
            return Task.CompletedTask;
        });

        // Act
        await using var debouncer = new EventCallbackDebouncer(callback);
        await debouncer.InvokeDebouncedAsync();
        await Task.Delay(150);

        // Assert
        await Assert.That(callCount).IsEqualTo(1);
    }

    [Test]
    public async Task CustomDebounceMs_ShouldRespectSpecifiedTime() {
        // Arrange
        int callCount = 0;
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => {
            callCount++;
            return Task.CompletedTask;
        });

        const int customDebounceMs = 200;

        // Act
        await using var debouncer = new EventCallbackDebouncer(callback, customDebounceMs);
        await debouncer.InvokeDebouncedAsync();
        await Task.Delay(150);// Less than debounce time

        // Assert
        await Assert.That(callCount).IsEqualTo(0);

        // Wait for remaining time
        await Task.Delay(100);
        await Assert.That(callCount).IsEqualTo(1);
    }

    [Test]
    public async Task MultipleInvocations_ShouldDebounce() {
        // Arrange
        int callCount = 0;
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => {
            callCount++;
            return Task.CompletedTask;
        });

        // Act
        await using var debouncer = new EventCallbackDebouncer(callback);
        await debouncer.InvokeDebouncedAsync();
        await debouncer.InvokeDebouncedAsync();
        await debouncer.InvokeDebouncedAsync();
        await Task.Delay(150);

        // Assert
        await Assert.That(callCount).IsEqualTo(1);
    }

    [Test]
    public async Task ConcurrentInvocations_ShouldBeThreadSafe() {
        // Arrange
        int callCount = 0;
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => {
            callCount++;
            return Task.CompletedTask;
        });

        // Act
        var debouncer = new EventCallbackDebouncer(callback);
        IEnumerable<Task> tasks = Enumerable.Range(0, 10)
            .Select(_ => debouncer.InvokeDebouncedAsync());

        await Task.WhenAll(tasks);
        await Task.Delay(150);

        // Assert
        await Assert.That(callCount).IsEqualTo(1);
        await debouncer.DisposeAsync();
    }

    [Test]
    public async Task AfterDispose_ShouldThrowObjectDisposedException() {
        // Arrange
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => Task.CompletedTask);
        var debouncer = new EventCallbackDebouncer(callback);

        // Act
        await debouncer.DisposeAsync();

        // Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            await debouncer.InvokeDebouncedAsync());
    }

    [Test]
    public async Task MultipleDispose_ShouldBeIdempotent() {
        // Arrange
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => Task.CompletedTask);
        var debouncer = new EventCallbackDebouncer(callback);

        // Act & Assert
        await debouncer.DisposeAsync();
        await debouncer.DisposeAsync();// Should not throw
    }

    [Test]
    public async Task InvocationDuringDebounce_ShouldCancelPrevious() {
        // Arrange
        var executionTimes = new List<DateTime>();
        EventCallback callback = EventCallback.Factory.Create(this, callback: () => {
            executionTimes.Add(DateTime.UtcNow);
            return Task.CompletedTask;
        });

        // Act
        await using var debouncer = new EventCallbackDebouncer(callback);
        await debouncer.InvokeDebouncedAsync();
        await Task.Delay(50);// Wait half the debounced time
        await debouncer.InvokeDebouncedAsync();
        await Task.Delay(150);

        // Assert
        await Assert.That(executionTimes).HasCount().EqualToOne();
    }
}
