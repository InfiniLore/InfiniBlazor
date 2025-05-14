// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IToastService>]
public class ToastService(IToastAppearanceProvider appearanceProvider) : IToastService {
    public event Func<Task>? OnChangeAsync;

    private readonly List<ToastMessage> _messages = new();
    public IEnumerable<IToastMessage> Messages => _messages;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ShowToastAsync(string title, int durationSeconds = 5, string? appearanceKey = null) {
        IToastAppearance appearance = appearanceProvider.GetAppearanceOrDefault(appearanceKey);
        var toast = new ToastMessage (title, durationSeconds, appearance);
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
