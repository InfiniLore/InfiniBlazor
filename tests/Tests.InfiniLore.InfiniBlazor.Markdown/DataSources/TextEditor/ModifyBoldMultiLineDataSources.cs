// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.TextEditor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ModifyBoldMultiLineDataSources {
    private const string SectionName = "bold";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<(string, int, int, string)>> HelloWoldDataSource() {
        const string input = "Hello World!";
        const string boldFormat = "**";

        for (int start = 0; start <= input.Length; start++) {
            for (int end = input.Length; end >= start; end--) {
                string modified = $"{input[..start]}{boldFormat}{input[start..end]}{boldFormat}{input[end..]}";
                int i = start;
                int j = end;
                yield return () => (SectionName, i, j, modified);
            }
        }
    }

    public static IEnumerable<Func<(string, string, string)>> DataSources() {
        yield return () => (
            SectionName,
            "- ",
            "- ****"
        );

        yield return () => (
            SectionName,
            "- Hello World!",
            "- **Hello World!**"
        );

        yield return () => (
            SectionName,
            "1. Hello World!",
            "1. **Hello World!**"
        );

        yield return () => (
            SectionName,
            """
            - Hello World!
            - Hello World!
            """,
            """
            - **Hello World!**
            - **Hello World!**
            """
        );

        yield return () => (
            SectionName,
            """
            1. Hello World!
            2. Hello World!
            """,
            """
            1. **Hello World!**
            2. **Hello World!**
            """
        );

        yield return () => (
            SectionName,
            """
            | test | something |
            | ---- | --------- |
            | alpha | beta |
            """,
            """
            | **test** | **something** |
            | ---- | --------- |
            | **alpha** | **beta** |
            """
        );

        yield return () => (
            SectionName,
            "alpha | beta |",
            "**alpha** | **beta** |"
        );

        yield return () => (
            SectionName,
            "alpha | beta",
            "**alpha** | **beta**"
        );

        yield return () => (
            SectionName,
            "| alpha | beta",
            "| **alpha** | **beta**"
        );
    }
}
