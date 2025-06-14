// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToastingProvider {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task PublishToastAsync(IToastData data, ToastAppearance appearance, int displayDuration = -1)
        => throw new NotImplementedException();

    public Task UnpublishToastAsync(Guid id)
        => throw new NotImplementedException();

    public IToastData CreateToastData(string title, string? body = null, string? linkHref = null, string? linkTitle = null)
        => new ToastData(title, body, linkHref, linkTitle);
}
