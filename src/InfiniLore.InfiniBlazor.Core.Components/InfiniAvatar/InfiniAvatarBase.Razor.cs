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
    [Parameter] public Size Size { get; set; } = Size.M;
    
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

    protected string SizeClasses => Size switch {
        Size.Xxs => "size-4",
        Size.Xs => "size-5",
        Size.S => "size-6.5",
        Size.M => "size-8",
        Size.L => "size-12",
        Size.Xl => "size-16",
        Size.Xxl => "size-20",
        _ => "size-8"
    };

    protected string PaddingClasses => Size switch {
        Size.Xxs => "m-0.5",
        Size.Xs => "m-1",
        Size.S => "m-1.5",
        Size.M => "m-2",
        Size.L => "m-2.5",
        Size.Xl => "m-3",
        Size.Xxl => "m-3.5",
        _ => "m-2"
    };

    private string StatusCircleClasses => Size switch {
        Size.Xxs => "size-2 border-none",
        Size.Xs => "size-3 border-2",
        Size.S => "size-3 border-1",
        Size.M => "size-4 border-2",
        Size.L => "size-6 border-3",
        Size.Xl => "size-7 border-4",
        Size.Xxl => "size-8 border-5",
        _ => "size-5 border-1.5"
    };

    private string StatusCustomClasses => Size switch {
        Size.Xxs => "size-4 text-xs bottom-0.25",
        Size.Xs => "size-5 text-sm",
        Size.S => "size-6 text-base left-0.25",
        Size.M => "size-7 text-lg left-0.5 top-0.25",
        Size.L => "size-8 text-xl bottom-0.5 top-0.1",
        Size.Xl => "size-8 text-2xl bottom-0.5",
        Size.Xxl => "size-9 text-2xl bottom-0.5",
        _ => "size-7 text-sm"
    };

    private string StatusCustomBackgroundClasses => Size switch {
        Size.Xxs => "size-4",
        Size.Xs => "size-5",
        Size.S => "size-6",
        Size.M => "size-6",
        Size.L => "size-7 bottom-0.5 right-0.5",
        Size.Xl => "size-8",
        Size.Xxl => "size-9",
        _ => "size-7 text-sm"
    };

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
