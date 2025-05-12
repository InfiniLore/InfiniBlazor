// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToastService : IToastService{
    public event Func<Task>? OnChangeAsync;

    private readonly List<ToastMessage> _messages = new();
    public IEnumerable<IToastMessage> Messages => _messages;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ShowToastAsync(string title, string message, int durationSeconds = 5) {
        var toast = new ToastMessage (
            IconName: "info",
            Title : title,
            Message : message,
            DurationSeconds : durationSeconds
        );

        _messages.Add(toast);
        if (OnChangeAsync is not null) await OnChangeAsync();
        if (durationSeconds is not -1) _ = AutoRemoveAsync(toast, durationSeconds);
    }

    private async Task RemoveToastAsync(Guid id) {
        ToastMessage? toast = _messages.FirstOrDefault(t => t.Id == id);
        if (toast == null) return;

        _messages.Remove(toast);
        OnChangeAsync?.Invoke();
    }

    private async Task AutoRemoveAsync(ToastMessage toast, int delaySeconds) {
        if (toast.Id == Guid.Empty) return;
        await Task.Delay(delaySeconds * 1000);
        await RemoveToastAsync(toast.Id);
    }
}
