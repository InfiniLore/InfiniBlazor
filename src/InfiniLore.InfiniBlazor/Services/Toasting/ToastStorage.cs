// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IToastStorage>]
public class ToastStorage : IToastStorage {
    public event Func<Task>? OnChangeAsync;

    private readonly List<IToastData> _messages = new();
    public IEnumerable<IToastData> Messages => _messages;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask AddToastAsync(IToastData toastMessage) {
        _messages.Add(toastMessage);
        if (OnChangeAsync is not null) await OnChangeAsync();
        if (toastMessage.DurationSeconds is not -1) _ = AutoRemoveAsync(toastMessage.Id, toastMessage.DurationSeconds);
    }
    
    private async Task AutoRemoveAsync(Guid toastId, int delaySeconds) {
        await Task.Delay(delaySeconds * 1000);
        await RemoveToastAsync(toastId);
    }
    
    public async ValueTask RemoveToastAsync(Guid toastId) {
        IToastData? toast = _messages.FirstOrDefault(x => x.Id == toastId);
        if (toast is null) return;
        if (toast.Id == Guid.Empty) return;
        if (!_messages.Remove(toast)) return;
        
        if (OnChangeAsync is null) return;
        await OnChangeAsync();
    }
    
    public void Clear()
        => _messages.Clear();
}
