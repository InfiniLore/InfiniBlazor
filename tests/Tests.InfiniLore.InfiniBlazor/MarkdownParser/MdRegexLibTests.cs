// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using System.Text.RegularExpressions;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdRegexLibTests {
    [Test]
    public async Task ShouldHaveUniqueGroupNames() {
        // Arrange
        Regex[] regexes = MdRegexLib.GetAllRegexes();

        HashSet<string> namesToSkip = [
            "open"
        ];
        
        HashSet<string> names = new();
        
        // Act
        string[] duplicates =  regexes
            .SelectMany(regex => regex.GetGroupNames())
            .Where(name => namesToSkip.Contains(name) && int.TryParse(name, out _) && !names.Add(name))
            .ToArray();
        
        // Assert
        await Assert.That(duplicates).IsEquivalentTo(Array.Empty<string>());
    }
}
