// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("strike")]
public class StrikeModifier(ILogger<StrikeModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "strikethrough";
    public override string ModifierName => "strike";
    protected override string Instruction => "~";
}
