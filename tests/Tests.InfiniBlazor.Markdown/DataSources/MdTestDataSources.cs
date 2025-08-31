// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using System.Runtime.CompilerServices;
using Tests.InfiniBlazor.Shared.Markdown;

namespace Tests.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestDataSources {
    public static IEnumerable<Func<MdTestData>> GetBlankTest() {
        yield return () => new MdTestData {
            FileName = "_",
            Id = "_",
            MdString = "",
            MdSyntaxTree = new MdSyntaxTree(),
            DeveloperNote = "Ensure Test is at least ran once to exist in the view",
            ExpectedHtmlStringSimplified = "",
            ExpectedMarkdownString = ""       
        };
    }
    
    public static async IAsyncEnumerable<Func<MdTestData>> GetTestDataAsync([EnumeratorCancellation] CancellationToken ct = default) {
        string[]? allFiles = MdTestDataProvider.TestInstance.TryGetFileNames();
        if (allFiles is null) throw new Exception("Could not get file names");

        foreach (string file in allFiles) {
            List<MdTestData>? dataEntries = await MdTestDataProvider.TestInstance.TryGetXmlMdTestDataAsync(file, ct);
            if (dataEntries is null) throw new Exception($"Could not get data for file {file}");
            
            foreach (MdTestData dataEntry in dataEntries) {
                yield return () => dataEntry;
            }

        }
    }
}
