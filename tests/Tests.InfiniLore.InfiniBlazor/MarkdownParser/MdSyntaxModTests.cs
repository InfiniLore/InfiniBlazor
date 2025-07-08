// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxModTests {
    [Test]
    [Arguments("|title=something")]
    [Arguments("|title=something|")]
    public async Task ShouldParseCorrectly_TryGetTitle(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxMod {
            Attributes = {
                ["title"] = new Range(7, 16)
            },
            OriginalInput = input
        };

        // Act
        MdSyntaxMod result = MdSyntaxMod.FromString(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedOutput);
        await Assert.That(result.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(result.Attributes["title"]).IsEqualTo(expectedOutput.Attributes["title"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.TryGetTitle(out string? title)).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
    
    [Test]
    [Arguments("|fit")]
    [Arguments("|fit|")]
    public async Task ShouldParseCorrectly_GetFit(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxMod {
            Attributes = {
                ["fit"] = new Range(4, 4)
            },
            OriginalInput = input
        };

        // Act
        MdSyntaxMod result = MdSyntaxMod.FromString(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedOutput);
        await Assert.That(result.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(result.Attributes["fit"]).IsEqualTo(expectedOutput.Attributes["fit"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.GetFit()).IsTrue();
    }
    
    
    [Test]
    [Arguments("|size=100x100")]
    [Arguments("|size=100x100|")]
    public async Task ShouldParseCorrectly_TryGetSize(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxMod {
            Attributes = {
                ["size"] = new Range(6, 13)
            },
            OriginalInput = input
        };

        // Act
        MdSyntaxMod result = MdSyntaxMod.FromString(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedOutput);
        await Assert.That(result.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(result.Attributes["size"]).IsEqualTo(expectedOutput.Attributes["size"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.TryGetSize(out (int Width, int Height) size)).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
    }
    
    [Test]
    [Arguments("|size=100x100|fit|title=something")]
    [Arguments("|size=100x100|fit|title=something|")]
    public async Task ShouldParseCorrectly_Multiple(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxMod {
            Attributes = {
                ["size"] = new Range(6, 13),
                ["fit"] = new Range(17, 17),
                ["title"] = new Range(24, 33)
            },
            OriginalInput = input
        };

        // Act
        MdSyntaxMod result = MdSyntaxMod.FromString(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedOutput);
        await Assert.That(result.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(result.Attributes["size"]).IsEqualTo(expectedOutput.Attributes["size"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.TryGetSize(out (int Width, int Height) size)).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
        
        await Assert.That(result.Attributes["fit"]).IsEqualTo(expectedOutput.Attributes["fit"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.GetFit()).IsTrue();
        
        await Assert.That(result.Attributes["title"]).IsEqualTo(expectedOutput.Attributes["title"]);
        await Assert.That(result.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result.TryGetTitle(out string? title)).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
    
    [Test]
    [Arguments("|size=100x100")]
    [Arguments("|size=100x100|")]
    public async Task ShouldParseCorrectly_TryGetAttributeValue(string input) {
        // Arrange
        MdSyntaxMod mod = MdSyntaxMod.FromString(input);

        // Act
        bool result = mod.TryGetAttributeValue("size", out string? value);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(value).IsEqualTo("100x100");
    }
}
