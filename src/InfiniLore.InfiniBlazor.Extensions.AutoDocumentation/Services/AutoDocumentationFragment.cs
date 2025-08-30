// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.AutoDocumentation;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record AutoDocumentationFragment(
    List<string> RazorDataList,
    List<string> CsharpDataList
) : IAutoDocumentationFragment {
    public IEnumerable<string> RazorData => RazorDataList;
    public IEnumerable<string> CsharpData => CsharpDataList;
}
