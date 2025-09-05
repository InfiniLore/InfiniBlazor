// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Editors.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>()]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
public class SuperscriptModifier(ILogger<SuperscriptModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName { get; } = "superscript";
    public override string ModifierName { get; } = "superscript";
    protected override string Instruction { get; } = "^^";
}
