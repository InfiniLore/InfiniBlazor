// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ComponentTestData {
    public static IEnumerable<Func<AutoDocumentationTestData>> GetTestData() {
        yield return () => new AutoDocumentationTestData("ComponentTest-SimpleParagraph", true, new AutoDocumentationFragment(
            ["<p>simple</p>"],
            []
        ));
        yield return () => new AutoDocumentationTestData("ComponentTest-NestedParagraph", true, new AutoDocumentationFragment(
            [
            """
            <p>
                <span>nested</span>
            </p>
            """
            ],
            []
        ));
        yield return () => new AutoDocumentationTestData("ComponentTest-Combo", true, new AutoDocumentationFragment(
            [
            """
            <button @onclick="@Click">Something</button>
            """
            ],
            [
            """
            public bool Click()
            {
                return true;
            }
            """
            ]
        ));
    }
}
