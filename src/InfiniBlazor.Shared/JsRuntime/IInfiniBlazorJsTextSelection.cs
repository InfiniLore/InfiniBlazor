// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IInfiniBlazorJsTextSelection {
    Task<int> GetStartIndexAsync(ElementReference element);
    Task<int> GetEndIndexAsync(ElementReference element);
    
    Task<(int, int)> GetAsTupleAsync(ElementReference element);
    Task<Range> GetRangeAsync(ElementReference element);
    
    Task SetRangeAsync(ElementReference element, int start, int end);
    Task SetRangeAsync(ElementReference element, Range range);
    Task SetIndexAsync(ElementReference element, int index);

}
