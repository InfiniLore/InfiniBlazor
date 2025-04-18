// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("bold")]
public class BoldModifier(ILogger<BoldModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "bold";
    public override string ModifierName => "bold";
    protected override string Instruction => "**";
}
