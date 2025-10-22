// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;
using Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources;
using Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.CodeFiles;
using Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.Components;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AutoDocumentationProviderTests {
    public AutoDocumentationProvider Provider { get; } = new([new AutoDocumenterData_TestsInfiniBlazorExtensionsAutoDocumentation()]);

    public sealed class AutoDocumentationFragmentComparer : IEqualityComparer<IAutoDocumentationFragment> {
        public static AutoDocumentationFragmentComparer Instance { get; } = new();

        public bool Equals(IAutoDocumentationFragment? x, IAutoDocumentationFragment? y) {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;

            return StringEnumerableEquals(x.RazorData, y.RazorData) && StringEnumerableEquals(x.CsharpData, y.CsharpData);
        }

        private static bool StringEnumerableEquals(IEnumerable<string>? enumerable1, IEnumerable<string>? enumerable2) {
            if (ReferenceEquals(enumerable1, enumerable2)) return true;
            if (enumerable1 is null || enumerable2 is null) return false;

            using IEnumerator<string> enumerator1 = enumerable1.GetEnumerator();
            using IEnumerator<string> enumerator2 = enumerable2.GetEnumerator();

            while (true) {
                bool hasNext1 = enumerator1.MoveNext();
                bool hasNext2 = enumerator2.MoveNext();

                if (hasNext1 != hasNext2) return false; // Different lengths
                if (!hasNext1) return true; // Both ended at the same time

                if (!StringEqualsIgnoringLineEndings(enumerator1.Current, enumerator2.Current)) {
                    return false;
                }
            }
        }

        private static bool StringEqualsIgnoringLineEndings(string? str1, string? str2) {
            if (ReferenceEquals(str1, str2)) return true;
            if (str1 is null || str2 is null) return false;

            // Normalize line endings to \n for comparison
            string normalized1 = str1.ReplaceLineEndings("\n");
            string normalized2 = str2.ReplaceLineEndings("\n");

            return normalized1.Equals(normalized2);
        }

        public int GetHashCode(IAutoDocumentationFragment obj) {
            return HashCode.Combine(
                GetStringEnumerableHashCode(obj.RazorData),
                GetStringEnumerableHashCode(obj.CsharpData)
            );
        }

        private static int GetStringEnumerableHashCode(IEnumerable<string>? enumerable) {
            if (enumerable is null) return 0;

            var hash = new HashCode();
            foreach (string str in enumerable) {
                string normalized = str.ReplaceLineEndings("\n");
                hash.Add(normalized);
            }

            return hash.ToHashCode();
        }

    }


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource<ClassTestData>(nameof(ClassTestData.GetTestData))]
    [MethodDataSource<PropertyTestData>(nameof(PropertyTestData.GetTestData))]
    [MethodDataSource<ComponentTestData>(nameof(ComponentTestData.GetTestData))]
    public async Task ShouldGetValidFragment(AutoDocumentationTestData testData) {
        // Arrange
        (string id, bool expectedResult, IAutoDocumentationFragment? expectedFragment) = testData;

        // Act
        bool result = Provider.TryGetDocumentationFragment(id, out IAutoDocumentationFragment? fragment);

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        await Assert.That(fragment).IsEqualTo(expectedFragment, AutoDocumentationFragmentComparer.Instance);
    }
}
