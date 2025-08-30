using InfiniLore.InfiniBlazor.AutoDocumentation;
using InfiniLore.Lucide;
using Microsoft.AspNetCore.Components.Web;

namespace Example.InfiniBlazor.Shared.Components.Pages.ExampleAutoDoc;
public partial class ExampleAutoDocPage {
    [AutoDocument("else")] private static void DoSomething(MouseEventArgs obj) {
        throw new NotImplementedException();
    }
    
    [AutoDocument("else")] public class BEHINDLetsDoATest {
        
    }
    
    [AutoDocument(LucideNames.Baby)] public record BEHINDLetsDoATestRecord {
        
    }
    
    
    [AutoDocument(LucideNames.Baby)] public struct BEHINDLetsDoATestStruct {
        
    }
    
    [AutoDocument(LucideNames.Baby)] public record struct BEHINDLetsDoATestRecordStruct {
        
    }
}
