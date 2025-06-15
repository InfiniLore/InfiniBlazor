// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Toasting;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components.ToastAppearances;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class ToastMessageBase: ComponentBase, IToastMessageBase, IDisposable {
    [Inject] protected IToastingProvider ToastingProvider { get; set; } = null!;
    
    [Parameter, EditorRequired] public IToastingData ToastData { get; set; } = ToastingData.Empty;
    [Parameter] public bool IsClosing { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static IDictionary<string, object> GetAsDynamicParameters(
        IToastingData toastData,
        bool isClosing = false
    )
        => new Dictionary<string, object> {
            [nameof(ToastData)] = toastData,
            [nameof(IsClosing)] = isClosing
        };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        ToastingProvider.AttachComponent(ToastData.Id, this);
    }

    public virtual async Task RequestCloseAsync() {
        if (IsClosing) return;
        IsClosing = true;
        StateHasChanged();
        await Task.Delay(300); // match Tailwind animation
        await ToastingProvider.UnpublishToastAsync(ToastData.Id);
    }
    
    public void Dispose() {
        ToastingProvider.DetachComponent(ToastData.Id);
        GC.SuppressFinalize(this);   
    }
}
