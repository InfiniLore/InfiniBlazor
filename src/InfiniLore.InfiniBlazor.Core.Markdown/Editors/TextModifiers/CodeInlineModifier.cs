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
public class CodeInlineModifier(ILogger<CodeInlineModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName { get; } = "code";
    public override string ModifierName { get; } = "code-inline";
    protected override string Instruction { get; } = "`";
}
