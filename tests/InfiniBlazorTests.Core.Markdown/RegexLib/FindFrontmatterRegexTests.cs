// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using System.Text.RegularExpressions;

namespace InfiniBlazorTests.Core.Markdown.RegexLib;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FindFrontmatterRegexTests {
    public record TestDataDto(bool ExpectedResult, string Input, string? Lang = null, string? Body = null);

    public static IEnumerable<Func<TestDataDto>> GetTestData() {
        yield return () => new TestDataDto(false, string.Empty);
        yield return () => new TestDataDto(false, " ");
        yield return () => new TestDataDto(false, "---");
        yield return () => new TestDataDto(false, "---test---");
        yield return () => new TestDataDto(
            false,
            """
            No longer a frontmatter.

            ---
            not a frontmatter
            ---
            """
        );

        yield return () => new TestDataDto(
            true,
            """
            ---
            something
            ---
            """, 
            null,
            "something"
        );
        
        yield return () => new TestDataDto(
            true,
            """
            ---json
            something
            ---
            """, 
            "json",
            "something"
        );
        
        yield return () => new TestDataDto(
            true,
            """
            --- json
            something
            ---
            """, 
            "json",
            "something"
        );
        
        yield return () => new TestDataDto(
            true,
            """
            --- json
            something
            across
            multiple lines
            ---
            """, 
            "json",
            """
            something
            across
            multiple lines
            """
        );

    }

    [Test]
    [MethodDataSource(nameof(GetTestData))]
    public async Task FindFrontmatterRegex_ShouldReturnExpected(TestDataDto testData) {
        // Arrange
        Regex regex = FrontmatterSyntaxNodeSerializer.Syntax;
        string input = testData.Input.ReplaceLineEndings("\n");
        bool expected = testData.ExpectedResult;
        
        bool langFound = testData.Lang != null;
        string expectedLang = testData.Lang?.ReplaceLineEndings("\n") ?? string.Empty;
        
        bool bodyFound = testData.Body != null;
        string expectedBody = testData.Body?.ReplaceLineEndings("\n") ?? string.Empty;

        // Act
        Match result = regex.Match(input);
        Group groupLang = result.Groups[MdRegexGroupNames.FrontmatterLang];
        Group groupBody = result.Groups[MdRegexGroupNames.FrontmatterBody];

        // Assert
        await Assert.That(result.Success).IsEqualTo(expected);
        await Assert.That(groupLang.Success).IsEqualTo(langFound);
        await Assert.That(groupLang.Value).IsEqualTo(expectedLang);
        await Assert.That(groupBody.Success).IsEqualTo(bodyFound);
        await Assert.That(groupBody.Value).IsEqualTo(expectedBody);

    }
}
