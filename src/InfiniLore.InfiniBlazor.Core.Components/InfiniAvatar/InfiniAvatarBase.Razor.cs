// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.Avatars;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ErrorEventArgs=Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract partial class InfiniAvatarBase : InfiniComponentBase {
    [Parameter] public AvatarData AvatarData { get; set; } = AvatarData.Empty;

    [Inject] public required HttpClient Http { get; set; }
    [Inject] public required ILogger<InfiniAvatarBase> Logger { get; set; }

    protected bool ImageLoadFailed;
    
    protected string StatusClasses => AvatarData.Status switch {
        AvatarStatus.Online => "bg-infini-green",
        AvatarStatus.Offline => "bg-infini-gray",
        AvatarStatus.Busy => "bg-infini-orange",
        AvatarStatus.DoNotDisturb => "bg-infini-red",
        _ => string.Empty
    };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnParametersSetAsync() {
        await TryVerifyImageUrl();
        await base.OnParametersSetAsync();
    }

    private async Task TryVerifyImageUrl() {
        string? imageUrl = AvatarData.ImageUrl;
        if (imageUrl.IsNullOrEmpty()) {
            ImageLoadFailed = true;
            return;
        }
        
        // Local path should always work
        if (imageUrl.StartsWith('/')) {
            ImageLoadFailed = false;
            return;
        }
        
        // External image could be a possible issue
        try {
            HttpResponseMessage response = await Http.GetAsync(imageUrl);
            if (!response.IsSuccessStatusCode) {
                Logger.Warning("Image {imageUrl} failed to load with status code {statusCode}", imageUrl, response.StatusCode);
                ImageLoadFailed = true;
            }
            else {
                ImageLoadFailed = false;
            }
        }
        catch (Exception ex) {
            Logger.Error(ex, "Error loading image {imageUrl}", imageUrl);
            ImageLoadFailed = true;
        }
    }
    
    protected void HandleImageError(ErrorEventArgs obj) {
        ImageLoadFailed = true;
    }
}
