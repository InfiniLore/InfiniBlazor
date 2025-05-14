// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IToastService>]
public class ToastService() : IToastService {
    public event Func<Task>? OnChangeAsync;

    private readonly List<IToastMessage> _messages = new();
    public IEnumerable<IToastMessage> Messages => _messages;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ShowToastAsync(IToastMessage toastMessage) {
        _messages.Add(toastMessage);
        if (OnChangeAsync is not null) await OnChangeAsync();
        if (toastMessage.DurationSeconds is not -1) _ = AutoRemoveAsync(toastMessage.Id, toastMessage.DurationSeconds);
    }
    
    private async Task AutoRemoveAsync(Guid toastId, int delaySeconds) {
        if (toastId == Guid.Empty) return;
        await Task.Delay(delaySeconds * 1000);
        await RemoveToastAsync(toastId);
    }
    
    public async Task RemoveToastAsync(Guid toastId) {
        IToastMessage? toast = _messages.FirstOrDefault(x => x.Id == toastId);
        if (toast is null) return;
        if (toast.Id == Guid.Empty) return;
        _messages.Remove(toast);
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
}
