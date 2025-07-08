// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifierExtensionsTests {
    [Test]
    [Arguments("|title=something")]
    [Arguments("|title=something|")]
    public async Task TryGetTitle(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxNodeModifier {
            Attributes = {
                ["title"] = new Range(7, 16)
            },
            OriginalInput = input
        };
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetTitle(out string? title);

        // Assert
        await Assert.That(mod.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(mod.Attributes["title"]).IsEqualTo(expectedOutput.Attributes["title"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
    
    [Test]
    [Arguments("|fit")]
    [Arguments("|fit|")]
    public async Task GetFit(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxNodeModifier {
            Attributes = {
                ["fit"] = new Range(4, 4)
            },
            OriginalInput = input
        };
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetFit(out bool fit);

        // Assert
        await Assert.That(mod.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(mod.Attributes["fit"]).IsEqualTo(expectedOutput.Attributes["fit"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result).IsTrue();
        await Assert.That(fit).IsTrue();
    }
    
    
    [Test]
    [Arguments("|size=100x100")]
    [Arguments("|size=100x100|")]
    public async Task TryGetSize(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxNodeModifier {
            Attributes = {
                ["size"] = new Range(6, 13)
            },
            OriginalInput = input
        };
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetSize(out (int Width, int Height) size);
        
        // Assert
        await Assert.That(mod.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(mod.Attributes["size"]).IsEqualTo(expectedOutput.Attributes["size"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(result).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
    }
    
    [Test]
    [Arguments("|size=100x100|fit|title=something")]
    [Arguments("|size=100x100|fit|title=something|")]
    public async Task Multiple(string input) {
        // Arrange
        var expectedOutput = new MdSyntaxNodeModifier {
            Attributes = {
                ["size"] = new Range(6, 13),
                ["fit"] = new Range(17, 17),
                ["title"] = new Range(24, 33)
            },
            OriginalInput = input
        };
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act

        // Assert
        await Assert.That(mod.Attributes).HasCount(expectedOutput.Attributes.Count);
        
        await Assert.That(mod.Attributes["size"]).IsEqualTo(expectedOutput.Attributes["size"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(mod.TryGetSize(out (int Width, int Height) size)).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
        
        await Assert.That(mod.Attributes["fit"]).IsEqualTo(expectedOutput.Attributes["fit"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(mod.TryGetFit(out bool fit)).IsTrue();
        await Assert.That(fit).IsTrue();
        
        await Assert.That(mod.Attributes["title"]).IsEqualTo(expectedOutput.Attributes["title"]);
        await Assert.That(mod.OriginalInput).IsEqualTo(expectedOutput.OriginalInput);
        await Assert.That(mod.TryGetTitle(out string? title)).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
}
