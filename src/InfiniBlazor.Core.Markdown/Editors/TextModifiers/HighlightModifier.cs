// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.TextEditor;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Editors.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
public class HighlightModifier(ILogger<HighlightModifier> logger) : SingleInstructionModifiers(logger) {
    public const string Name = "highlight";
    
    public override string ModifierName { get; } = Name;
    protected override string Instruction { get; } = "==";
}
