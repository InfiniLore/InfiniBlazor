// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.RegexLib;

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
