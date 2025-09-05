// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniBlazor.Core.Markdown.Syntax;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifierExtensionsTests {
    [Test]
    [Arguments("|title=something")]
    [Arguments("|title=something|")]
    public async Task TryGetTitle(string input) {
        // Arrange
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetTitle(out string? title);

        // Assert
        await Assert.That(mod.Attributes).HasCount(1);
        
        await Assert.That(mod.Attributes["title"]).IsEqualTo(new Range(7, 16));
        await Assert.That(result).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
    
    [Test]
    [Arguments("|fit")]
    [Arguments("|fit|")]
    public async Task GetFit(string input) {
        // Arrange
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetFit(out bool fit);

        // Assert
        await Assert.That(mod.Attributes).HasCount(1);
        
        await Assert.That(mod.Attributes["fit"]).IsEqualTo(new Range(4, 4));
        await Assert.That(result).IsTrue();
        await Assert.That(fit).IsTrue();
    }
    
    
    [Test]
    [Arguments("|size=100x100")]
    [Arguments("|size=100x100|")]
    public async Task TryGetSize(string input) {
        // Arrange
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act
        bool result = mod.TryGetSize(out (int Width, int Height) size);
        
        // Assert
        await Assert.That(mod.Attributes).HasCount(1);
        
        await Assert.That(mod.Attributes["size"]).IsEqualTo(new Range(6, 13));
        await Assert.That(result).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
    }
    
    [Test]
    [Arguments("|size=100x100|fit|title=something")]
    [Arguments("|size=100x100|fit|title=something|")]
    public async Task Multiple(string input) {
        // Arrange
        MdSyntaxNodeModifier mod = MdSyntaxNodeModifier.FromString(input);

        // Act

        // Assert
        await Assert.That(mod.Attributes).HasCount(3);
        
        await Assert.That(mod.Attributes["size"]).IsEqualTo(new Range(6, 13));
        await Assert.That(mod.TryGetSize(out (int Width, int Height) size)).IsTrue();
        await Assert.That(size).IsEqualTo((100, 100));
        
        await Assert.That(mod.Attributes["fit"]).IsEqualTo(new Range(17, 17));
        await Assert.That(mod.TryGetFit(out bool fit)).IsTrue();
        await Assert.That(fit).IsTrue();
        
        await Assert.That(mod.Attributes["title"]).IsEqualTo(new Range(24, 33));
        await Assert.That(mod.TryGetTitle(out string? title)).IsTrue();
        await Assert.That(title).IsEqualTo("something");
    }
}
