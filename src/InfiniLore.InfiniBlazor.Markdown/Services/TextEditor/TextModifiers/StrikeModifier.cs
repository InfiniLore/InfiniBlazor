// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("strike")]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
public class StrikeModifier(ILogger<StrikeModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName { get; } = "strikethrough";
    public override string ModifierName { get; } = "strike";
    protected override string Instruction { get; } = "~";
}
