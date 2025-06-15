// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IToastingProvider {
    event Func<Task> OnToastPublished;
    event Func<Task> OnToastUnpublished;
    
    Task PublishToastAsync(IToastingData data, ToastAppearance appearance, int displayDuration = -1);
    Task UnpublishToastAsync(Guid id);
    IToastingData CreateToastData(string title, string? body = null, string? linkHref = null, string? linkTitle = null);
    
    void AttachComponent(Guid id, IToastMessageBase component);
    void DetachComponent(Guid id);
    
    IEnumerable<IToastEntry> GetToastEntries();
}
