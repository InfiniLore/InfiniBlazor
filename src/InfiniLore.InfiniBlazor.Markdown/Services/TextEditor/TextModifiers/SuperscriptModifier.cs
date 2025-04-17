// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("superscript")]
public class SuperscriptModifier(ILogger<SuperscriptModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "superscript";
    public override string ModifierName => "superscript";
    protected override string Instruction => "^^";
}
