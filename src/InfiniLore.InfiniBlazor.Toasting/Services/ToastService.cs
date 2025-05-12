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
        if (durationSeconds is not -1) _ = AutoRemoveAsync(toast.Id, durationSeconds);
    }
    
    private async Task AutoRemoveAsync(Guid toastId, int delaySeconds) {
        if (toastId == Guid.Empty) return;
        await Task.Delay(delaySeconds * 1000);
        
        ToastMessage? toast = _messages.FirstOrDefault(x => x.Id == toastId);
        if (toast is null) return;
        if (toast.Id == Guid.Empty) return;
        if (toast.DurationSeconds is -1) return;
        _messages.Remove(toast);
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
}
