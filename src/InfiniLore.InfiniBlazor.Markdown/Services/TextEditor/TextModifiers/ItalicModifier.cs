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
[InjectableSingleton<ITextModifier>("italic")]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
public class ItalicModifier(ILogger<ItalicModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName { get; } = "italic";
    public override string ModifierName { get; } = "italic";
    protected override string Instruction { get; } = "*";
}
