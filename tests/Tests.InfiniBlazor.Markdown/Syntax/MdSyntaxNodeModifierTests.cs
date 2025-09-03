// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniBlazor.Markdown.Syntax;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifierTests {

    public static IEnumerable<Func<(string Input, int Count, string[][] ExpectedOutput)>> DataSources() {
        yield return () => ("", 0, []);
        yield return () => ("|", 0, []);
        yield return () => ("|=", 0, []);
        yield return () => ("|size=100x100", 1, []);
        yield return () => ("|size=100x100|", 1, [["size", "100x100"]]);

        yield return () => ("|size=100x100|something=else",2, [["size", "100x100"], ["something", "else"]]);
        yield return () => ("|size=100x100|something=else|",2, [["size", "100x100"], ["something", "else"]]);
        yield return () => ("|size=100x100|something=else|flag",3, [["size", "100x100"], ["something", "else"], ["flag"]]);
        yield return () => ("|size=100x100|something=else|flag|",3, [["size", "100x100"], ["something", "else"], ["flag"]]);
        
        // On duplicate keys the default behavior should be to take the last one.
        yield return () => ("|something=else|something=different",1, [["something", "different"]]);
        yield return () => ("|something=else|something=different|",1, [["something", "different"]]);
        
        // Test Escaped pipes
        yield return () => (@"|something=\||",1, [["something", @"\|"]]);
        yield return () => (@"|something=\\||",1, [["something", @"\\"]]);
        yield return () => (@"|something=\\\||",1, [["something", @"\\\|"]]);
        yield return () => ("|something=||",1, [["something", ""]]);
    }
    
    [Test]
    [MethodDataSource(nameof(DataSources))]
    public async Task ShouldParseCorrectly(string input, int count, string[][] expectedOutput) {
        // Arrange
        
        // Act
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Assert
        await Assert.That(mod.Attributes).HasCount(count);
        
        await Parallel.ForEachAsync(expectedOutput, async (expected, _) => {
            if (expected.Length == 2) {
                string attributeKey = expected.First();
                string attributeValue = expected.Last();

                bool result = mod.TryGetValue(attributeKey, out string? value);
     
                await Assert.That(result).IsTrue();
                await Assert.That(value).IsEqualTo(attributeValue);   
            }
            
            else {
                string attributeKey = expected.First();
                bool result = mod.TryGetFlag(attributeKey, out bool value);
     
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
        bool result = mod.TryGetFlag("flag", out bool foundState);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(foundState).IsEqualTo(expectedState);   
    }
}
