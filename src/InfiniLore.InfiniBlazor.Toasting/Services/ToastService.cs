// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToastService : IToastService{
    public event Action? OnChange;

    private readonly List<ToastMessage> _messages = new();
    public IEnumerable<IToastMessage> Messages => _messages;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task ShowToastAsync(string title, string message, int durationSeconds = 5) {
        var toast = new ToastMessage (
            IconName: "info",
            Title : title,
            Message : message,
            DurationSeconds : durationSeconds
        );

        _messages.Add(toast);
        OnChange?.Invoke();

        if (durationSeconds is not -1) _ = AutoRemoveAsync(toast, durationSeconds);
        return Task.CompletedTask;
    }

    private void RemoveToast(Guid id) {
        ToastMessage? toast = _messages.FirstOrDefault(t => t.Id == id);
        if (toast == null) return;

        _messages.Remove(toast);
        OnChange?.Invoke();
    }

    private async Task AutoRemoveAsync(ToastMessage toast, int delaySeconds) {
        await Task.Delay(delaySeconds * 1000);
        RemoveToast(toast.Id);
    }
}
