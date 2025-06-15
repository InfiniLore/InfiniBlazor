// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Timers;
using Timer=System.Timers.Timer;

namespace InfiniLore.InfiniBlazor.Toasting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IToastingProvider>]
public class ToastingProvider(IToastingConfig toastingConfig, ILogger<ToastingProvider> logger) : IToastingProvider {
    public event Func<Task>? OnToastPublished;
    public event Func<Task>? OnToastUnpublished;

    private ConcurrentDictionary<Guid, ToastingEntry> Toasts { get; } = new();
    private ConcurrentQueue<Guid> ToastOrder { get; } = new();
    private ConcurrentDictionary<Guid, Timer> RemovalTimers { get; } = new();
    private ConcurrentDictionary<Guid, IToastMessageBase> ToastMessageComponents { get; } = new();
    private readonly ConcurrentDictionary<Guid, bool> _closingToasts = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task PublishToastAsync(IToastingData data, ToastAppearance appearance, int displayDuration = -1) {
        if (displayDuration < -1) {
            logger.Warning("Invalid display duration: {duration}", displayDuration);
            return;
        }

        // -1 means: use default config duration
        displayDuration = displayDuration == -1
            ? toastingConfig.AutoRemoveDuration
            : displayDuration;

        if (!toastingConfig.AppearanceComponentMapping.TryGetValue(appearance, out Type? componentType)) {
            logger.Warning("Invalid toast appearance: {appearance}", appearance);
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
            if (OnToastPublished is not null) await OnToastPublished().ConfigureAwait(false);
            return;
        }

        // Timer-based auto-removal
        var timer = new Timer(displayDuration) { AutoReset = false };
        ElapsedEventHandler? handler = null;

        handler = (_, _) => {
            timer.Elapsed -= handler;
            _ = Task.Run(async () => {
                try {
                    await UnpublishToastAsync(data.Id);
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

        if (OnToastPublished is not null) await OnToastPublished().ConfigureAwait(false);
    }

    public async Task UnpublishToastAsync(Guid id) {
        // Attempt to mark this toast as closing; if already closing, skip
        if (!_closingToasts.TryAdd(id, true)) return;// Already closing, no need to continue

        try {
            if (RemovalTimers.TryRemove(id, out Timer? timer)) timer.Dispose();
            if (ToastMessageComponents.TryGetValue(id, out IToastMessageBase? component)) await component.RequestCloseAsync().ConfigureAwait(false);
            if (!Toasts.TryRemove(id, out _)) return; // Already removed, no need to continue

            var newQueue = new ConcurrentQueue<Guid>();
            foreach (Guid existingId in ToastOrder) {
                if (existingId != id)
                    newQueue.Enqueue(existingId);
            }

            while (!ToastOrder.IsEmpty) ToastOrder.TryDequeue(out _);
            foreach (Guid remainingId in newQueue) ToastOrder.Enqueue(remainingId);

            if (OnToastUnpublished is not null)
                await OnToastUnpublished().ConfigureAwait(false);

        }
        finally {
            // Remove from a closing set so future calls could work if toast is re-added
            _closingToasts.TryRemove(id, out _);
        }
    }

    public IToastingData CreateToastData(string title, string? body = null, string? linkHref = null, string? linkTitle = null)
        => new ToastingData(title, body, linkHref, linkTitle);

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
