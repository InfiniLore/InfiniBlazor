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
        themeConfig.AddToastSetupData(StandardToastAppearance.Info, new ToastAppearance("info", "bg-blue-100 border border-blue-400 text-blue-900 hover:bg-blue-200", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Success, new ToastAppearance("circle-check", "bg-green-100 border border-green-500 text-green-900 hover:bg-green-200", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Warning, new ToastAppearance("circle-help", "bg-yellow-100 border border-yellow-400 text-yellow-900 hover:bg-yellow-200", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Error, new ToastAppearance("circle-alert", "bg-red-100 border border-red-400 text-red-900 hover:bg-red-200", true));
        
        configure?.Invoke(themeConfig);
        
        config.Services.AddSingleton<IToastConfig>(themeConfig);
    }
}
