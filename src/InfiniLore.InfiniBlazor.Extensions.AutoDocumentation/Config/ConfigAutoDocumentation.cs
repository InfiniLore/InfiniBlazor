// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.AutoDocumentation;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class AutoDocumentationConfig(InfiniBlazorConfig infiniBlazorConfig) {
    
    // ReSharper disable once UnusedMethodReturnValue.Global
    public AutoDocumentationConfig RegisterAutoDocumentationData<TData>()
        where TData : class, IAutoDocumenterData, new() {
        infiniBlazorConfig.Services.AddSingleton<IAutoDocumenterData, TData>();
        return this;
    }
    
}