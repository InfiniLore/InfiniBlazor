// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifierTests {
    [Test]
    [Arguments("|size=100x100",1)]
    [Arguments("|size=100x100|",1, new[] {"size", "100x100"})]
    [Arguments("|size=100x100|something=else",2, new[] {"size", "100x100"}, new[] {"something", "else"})]
    [Arguments("|size=100x100|something=else|",2, new[] {"size", "100x100"}, new[] {"something", "else"})]
    [Arguments("|size=100x100|something=else|flag",3, new[] {"size", "100x100"}, new[] {"something", "else"}, new[] {"flag"})]
    [Arguments("|size=100x100|something=else|flag|",3, new[] {"size", "100x100"}, new[] {"something", "else"}, new[] {"flag"})]
    
    // On duplicate keys the default behavior should be to take the last one.
    [Arguments("|something=else|something=different",1, new[] {"something", "different"})]
    [Arguments("|something=else|something=different|",1, new[] {"something", "different"})]
    public async Task ShouldParseCorrectly(string input, int count, params string[][] expectedOutput) {
        // Arrange
        
        // Act
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Assert
        await Assert.That(mod.Attributes).HasCount(count);
        
        await Parallel.ForEachAsync(expectedOutput, async (expected, _) => {
            if (expected.Length == 2) {
                string attributeKey = expected.First();
                string attributeValue = expected.Last();

                bool result = mod.TryGetAttributeValue(attributeKey, out string? value);
     
                await Assert.That(result).IsTrue();
                await Assert.That(value).IsEqualTo(attributeValue);   
            }
            
            else {
                string attributeKey = expected.First();
                bool result = mod.TryGetAttributeFlag(attributeKey, out bool value);
     
                await Assert.That(result).IsTrue();
                await Assert.That(value).IsTrue();   
            }
        });
    }

    [Test]
    [Arguments("|flag",true)]
    [Arguments("|flag|",true)]
    [Arguments("|flag=true",true)]
    [Arguments("|flag=true|",true)]
    [Arguments("|flag=false",false)]
    [Arguments("|flag=false|",false)]
    [Arguments("|flag=1",true)]
    [Arguments("|flag=1|",true)]
    [Arguments("|flag=0",false)]
    [Arguments("|flag=0|",false)]
    public async Task TryGetAttributeFlag(string input, bool expectedState) {
        // Arrange
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetAttributeFlag("flag", out bool foundState);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(foundState).IsEqualTo(expectedState);   
    }
}
