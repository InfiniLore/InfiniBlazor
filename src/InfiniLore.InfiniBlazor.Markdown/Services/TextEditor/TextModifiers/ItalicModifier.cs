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
[InjectableSingleton<ITextModifier>("italic")]
public class ItalicModifier(ILogger<ItalicModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "italic";
    public override string ModifierName => "italic";
    protected override string Instruction => "*";
}
