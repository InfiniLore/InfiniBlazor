// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Toasting.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddToastingLogic(this IInfiniBlazorConfig config, Action<ToastConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorToasting();
        config.Services.AddSingleton<IToastService, ToastService>();
        
        var themeConfig = new ToastConfig(config);
        
        configure?.Invoke(themeConfig);
        
        config.Services.AddSingleton<IToastConfig>(themeConfig);
    }
}
