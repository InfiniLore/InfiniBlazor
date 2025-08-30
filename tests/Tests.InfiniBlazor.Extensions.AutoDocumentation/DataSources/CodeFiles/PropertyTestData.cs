// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.CodeFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class PropertyHoldingClass {
    [AutoDocument("PropertyTest")] public bool SomeProperty { get; set; }
    
}

public class PropertyTestData {
    private const string ExpectedSourceProperty = "public bool SomeProperty { get; set; }";
    

    public static IEnumerable<Func<AutoDocumentationTestData>> GetTestData() {
        yield return () => new AutoDocumentationTestData("PropertyTest", true, new AutoDocumentationFragment([], [ExpectedSourceProperty]));
    }
}
