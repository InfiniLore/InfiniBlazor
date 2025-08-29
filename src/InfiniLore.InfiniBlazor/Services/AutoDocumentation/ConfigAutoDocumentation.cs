// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.AutoDocumentation;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ConfigAutoDocumentation {
    public static InfiniBlazorConfig RegisterAutoDocumentationData<TData>(this InfiniBlazorConfig config)
        where TData : class, IAutoDocumenterData, new() {
        config.Services.AddSingleton<IAutoDocumenterData, TData>();
        return config;
    }
}
