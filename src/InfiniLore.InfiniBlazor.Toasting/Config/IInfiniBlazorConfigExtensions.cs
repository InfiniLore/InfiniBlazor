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
        config.Services.AddSingleton<IToastStorage, ToastStorage>();
        
        var themeConfig = new ToastConfig(config);
        themeConfig.AddToastSetupData(StandardToastAppearance.Info, new ToastAppearance("info", "bg-blue-950 border border-blue-600 text-blue-500 hover:bg-blue-900", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Success, new ToastAppearance("circle-check", "bg-green-950 border border-green-600 text-green-500 hover:bg-green-900", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Warning, new ToastAppearance("circle-help", "bg-yellow-800 border border-yellow-400 text-yellow-500 hover:bg-yellow-700", false));
        themeConfig.AddToastSetupData(StandardToastAppearance.Error, new ToastAppearance("circle-alert", "bg-rose-950 border border-red-600 text-red-500 hover:bg-rose-900", true));
        
        configure?.Invoke(themeConfig);
        
        config.Services.AddSingleton<IToastConfig>(themeConfig);
    }
}
