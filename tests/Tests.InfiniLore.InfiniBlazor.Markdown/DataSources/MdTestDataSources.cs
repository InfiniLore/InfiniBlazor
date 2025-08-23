// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Runtime.CompilerServices;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestDataSources {
    public static async IAsyncEnumerable<Func<MdTestData>> GetAsyncTestData([EnumeratorCancellation] CancellationToken ct = default) {
        string[]? allFiles = MdTestDataProvider.TestInstance.TryGetFileNames();
        if (allFiles is null) {
            yield break;
        }

        foreach (string file in allFiles) {
            List<MdTestData>? dataEntries = await MdTestDataProvider.TestInstance.TryGetXmlMdTestDataAsync(file, ct);
            if (dataEntries is null) {
                yield break;
            }

            foreach (MdTestData dataEntry in dataEntries) {
                yield return () => dataEntry;
            }

        }
    }
}
