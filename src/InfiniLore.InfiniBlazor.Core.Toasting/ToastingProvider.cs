// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Timers;
using Timer=System.Timers.Timer;

namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IToastingProvider>]
public class ToastingProvider(IToastingConfig toastingConfig, ILogger<ToastingProvider> logger) : IToastingProvider {
    public event Func<Task>? OnToastPublishedAsync;
    public event Func<Task>? OnToastUnpublishedAsync;

    private ConcurrentDictionary<Guid, ToastingEntry> Toasts { get; } = new();
    private ConcurrentQueue<Guid> ToastOrder { get; } = new();
    private ConcurrentDictionary<Guid, Timer> RemovalTimers { get; } = new();
    private ConcurrentDictionary<Guid, IToastMessageBase> ToastMessageComponents { get; } = new();
    private readonly ConcurrentDictionary<Guid, bool> _closingToasts = new();
    
    private FrozenDictionary<string, Type> AppearanceComponentMappings { get; } = toastingConfig.GetAppearanceComponentMapping();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task PublishToastAsync(IToastingData data, ToastAppearance appearance, int displayDuration = -1) 
        => PublishToastAsync(data, appearance.ToName(), displayDuration);
    
    public async Task PublishToastAsync(IToastingData data, string appearanceName, int displayDuration = -1) {
        if (displayDuration < -1) {
            logger.Warning("Invalid display duration: {duration}", displayDuration);
            return;
        }

        // -1 means: use default config duration
        displayDuration = displayDuration == -1
            ? toastingConfig.AutoRemoveDuration
            : displayDuration;

        if (!AppearanceComponentMappings.TryGetValue(appearanceName, out Type? componentType)) {
            logger.Warning("Invalid toast appearance: {appearance}", appearanceName);
            return;
        }

        Toasts.AddOrUpdate(
            data.Id,
            addValueFactory: _ => new ToastingEntry(data, componentType),
            updateValueFactory: (_, _) => new ToastingEntry(data, componentType)
        );

        ToastOrder.Enqueue(data.Id);

        // 0 means: never auto-remove
        if (displayDuration == 0) {
            if (OnToastPublishedAsync is not null) await OnToastPublishedAsync().ConfigureAwait(false);
            return;
        }

        // Timer-based auto-removal
        var timer = new Timer(displayDuration) { AutoReset = false };
        ElapsedEventHandler? handler = null;

        handler = (_, _) => {
            timer.Elapsed -= handler;
            _ = Task.Run(async () => {
                try {
                    // Pass true for isTimerInitiated to avoid calling RequestCloseAsync
                    await UnpublishToastAsync(data.Id, true);
                }
                catch {
                    // optionally log
                }
                finally {
                    timer.Dispose();
                }
            });
        };
        
        timer.Elapsed += handler;
        RemovalTimers[data.Id] = timer;
        timer.Start();

        if (OnToastPublishedAsync is not null) await OnToastPublishedAsync().ConfigureAwait(false);
    }

    public Task UnpublishToastAsync(Guid id) => UnpublishToastAsync(id, true);

    private async Task UnpublishToastAsync(Guid id, bool publishEvent) {
        // Attempt to mark this toast as closing; if already closing, skip
        if (!_closingToasts.TryAdd(id, true)) {
            logger.Debug("Toast is already closing: {id}", id);
            return;
        }

        try {
            if (RemovalTimers.TryRemove(id, out Timer? timer)) timer.Dispose();
        
            // Only call RequestCloseAsync for user-initiated closes, not timer-initiated
            if (ToastMessageComponents.TryGetValue(id, out IToastMessageBase? component)) {
                await component.RequestCloseFromProviderAsync().ConfigureAwait(false);
            }
        
            if (!Toasts.TryRemove(id, out _)) {
                logger.Warning("Could not remove toast: {id}", id);
                return;           
            }

            var newQueue = new ConcurrentQueue<Guid>();
            foreach (Guid existingId in ToastOrder) {
                if (existingId != id)
                    newQueue.Enqueue(existingId);
            }

            while (!ToastOrder.IsEmpty) ToastOrder.TryDequeue(out _);
            foreach (Guid remainingId in newQueue) ToastOrder.Enqueue(remainingId);

            if (OnToastUnpublishedAsync is not null && publishEvent)
                await OnToastUnpublishedAsync().ConfigureAwait(false);

        }
        finally {
            // Remove from a closing set so future calls could work if toast is re-added
            _closingToasts.TryRemove(id, out _);
        }
    }


    public async Task UnpublishAllToastsAsync() {
        Guid[] ids = ToastOrder.ToArray();
        await Task.WhenAll(ids.Select(id => UnpublishToastAsync(id, false) )).ConfigureAwait(false);
        
        // Only send the unpublished event ONCE.
        if (OnToastUnpublishedAsync is not null)
            await OnToastUnpublishedAsync().ConfigureAwait(false);
    }

    public void AttachComponent(Guid id, IToastMessageBase component) {
        ToastMessageComponents.AddOrUpdate(
            id,
            addValueFactory: _ => component,
            updateValueFactory: (_, _) => component
        );
    }
    public void DetachComponent(Guid id) {
        ToastMessageComponents.TryRemove(id, out _);
    }

    public IEnumerable<IToastEntry> GetToastEntries() {
        foreach (Guid id in ToastOrder) {
            if (!Toasts.TryGetValue(id, out ToastingEntry? entry)) continue;

            yield return entry;
        }
    }
}
