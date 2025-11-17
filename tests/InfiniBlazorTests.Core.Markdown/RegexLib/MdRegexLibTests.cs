// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;

namespace InfiniBlazorTests.Core.Markdown.RegexLib;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdRegexLibTests {
    [Test]
    public async Task ShouldHaveUniqueGroupNames() {
        // Arrange
        Regex[] regexes = MdRegexLib.GetAllRegexes();
        HashSet<string> names = new();
        
        // Act
        HashSet<string> duplicates = regexes
            .SelectMany(regex => regex.GetGroupNames())
            .Where(name => !int.TryParse(name, out _) && !names.Add(name))
            .ToHashSet();
        
        // Assert
        await Assert.That(duplicates).IsEquivalentTo(["open"]); // only used for balancing groups
    }
}
