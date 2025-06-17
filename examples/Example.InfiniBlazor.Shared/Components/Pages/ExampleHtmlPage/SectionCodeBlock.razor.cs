// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Example.InfiniBlazor.Shared.Components.Pages.ExampleHtmlPage;
// -----------------------------------------------------------------------------------------------------------------
// Methods
// -----------------------------------------------------------------------------------------------------------------
public partial class SectionCodeBlock {
    private string CSharpCode { get; set; } = """
        var example = "Hello, World!";var example = "Hello, World!";var example = "Hello, World!";var example = "Hello, World!";var example = "Hello, World!";var example = "Hello, World!";var example = "Hello, World!";
        Console.WriteLine(example);
        
        if (something){
            await DoSomething();
        }
        """;
}
