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
[InjectableSingleton<ITextModifier>("subscript")]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
public class SubscriptModifier(ILogger<SubscriptModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName { get; } = "subscript";
    public override string ModifierName { get; } = "subscript";
    protected override string Instruction { get; } = "^";
}
