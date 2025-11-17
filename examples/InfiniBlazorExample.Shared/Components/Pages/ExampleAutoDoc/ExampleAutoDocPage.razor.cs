// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniBlazorExample.Shared.Components.Pages.ExampleAutoDoc;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class ExampleAutoDocPage {
    [AutoDocument("else")] private static void DoSomething(MouseEventArgs obj) {
        // Does Something
    }
}
