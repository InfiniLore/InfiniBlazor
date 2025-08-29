using InfiniLore.InfiniBlazor.AutoDocumenting;
using Microsoft.AspNetCore.Components.Web;

namespace Example.InfiniBlazor.Shared.Components.Pages.ExampleAutoDoc;
public partial class ExampleAutoDocPage {
    [AutoDocument("else")] private void DoSomething(MouseEventArgs obj) {
        throw new NotImplementedException();
    }
}
