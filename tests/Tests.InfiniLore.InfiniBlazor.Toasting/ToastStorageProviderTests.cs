// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Toasting;
using Tests.InfiniLore.InfiniBlazor.Toasting.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class ToastStorageProviderTests(IToastStorage storage) {
    private bool OnChangeTriggered { get; set; }
    private Task AddMessageOnChangeAsync() {
        OnChangeTriggered = true;
        return Task.CompletedTask;
    }
    private Task RemoveMessageOnChangeAsync() {
        OnChangeTriggered = true;
        return Task.CompletedTask;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [After(Test)]
    public void ResetOnChangeTriggered() {
        storage.OnChangeAsync -= AddMessageOnChangeAsync;
        storage.OnChangeAsync -= RemoveMessageOnChangeAsync;
        storage.Clear();
    }
    
    [Test]
    public async Task AddMessage_ShouldWork() {
        // Arrange
        var message = new ToastData(
            "ADD MESSAGE TEST",
            -1,
            ToastAppearance.Default
        );
        storage.OnChangeAsync += AddMessageOnChangeAsync;
        
        // Act
        await storage.AddToastAsync(message);
        
        // Assert
        await Assert.That(OnChangeTriggered).IsTrue();
        await Assert.That(storage.Messages).IsNotEmpty()
            .And.Contains(foundMessage => ReferenceEquals(foundMessage, message));
    }
    
    [Test]
    public async Task RemoveMessage_ShouldWork() {
        // Arrange
        var message = new ToastData(
            "REMOVE MESSAGE TEST",
            -1,
            ToastAppearance.Default
        );
        await storage.AddToastAsync(message);
        storage.OnChangeAsync += RemoveMessageOnChangeAsync;
        
        // Act
        await storage.RemoveToastAsync(message.Id);
        
        // Assert
        await Assert.That(OnChangeTriggered).IsTrue();
        await Assert.That(storage.Messages).IsEmpty();
    }
}
