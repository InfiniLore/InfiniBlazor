// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("subscript")]
public class SubscriptModifier(ILogger<SubscriptModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "subscript";
    public override string ModifierName => "subscript";
    protected override string Instruction => "^";
}
