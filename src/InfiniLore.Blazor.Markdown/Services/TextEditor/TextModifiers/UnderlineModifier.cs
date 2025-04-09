// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.Blazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("underline")]
public class UnderlineModifier(ILogger<UnderlineModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "underline";
    public override string ModifierName => "underline";
    protected override string Instruction => "_";
}
