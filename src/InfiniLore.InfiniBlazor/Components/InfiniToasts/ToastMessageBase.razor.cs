// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Toasting;
using InfiniLore.Lucide;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class ToastMessageBase : InfiniComponentBase, IToastMessageBase, IDisposable {
    [Inject] protected IToastingProvider ToastingProvider { get; set; } = null!;
    [Parameter, EditorRequired] public IToastingData ToastData { get; set; } = ToastingData.Empty;
    
    protected virtual string HeaderClasses => "text-(--text)";
    protected virtual string BodyClasses => "infini-bg-(--color-base-90) border-(--border) text-(--text)";
    protected virtual string IconName => LucideNames.CircleQuestionMark;

    private bool IsClosing { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static IDictionary<string, object> GetAsDynamicParameters(IToastingData toastData)
        => new Dictionary<string, object> {
            [nameof(ToastData)] = toastData
        };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        ToastingProvider.AttachComponent(ToastData.Id, this);
    }

    protected virtual async Task RequestCloseAsync() {
        if (IsClosing) return;
        IsClosing = true;
        await InvokeAsync(StateHasChanged);
        await Task.Delay(300); // match Tailwind animation
        await ToastingProvider.UnpublishToastAsync(ToastData.Id);
    }

    public virtual async Task RequestCloseFromProviderAsync() {
        if (IsClosing) return;
        IsClosing = true;
        await InvokeAsync(StateHasChanged);
        await Task.Delay(300); // match Tailwind animation
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose() {
        ToastingProvider.DetachComponent(ToastData.Id);
        GC.SuppressFinalize(this);   
    }
    
    private static bool IsRelativeUri(string uriString) {
        if (uriString.IsNullOrWhiteSpace()) return false;
        if (!Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out Uri? uri)) return false;

        return !uri.IsAbsoluteUri;
    }
}
