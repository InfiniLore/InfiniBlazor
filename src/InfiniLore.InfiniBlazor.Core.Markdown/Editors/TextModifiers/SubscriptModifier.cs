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
public class SubscriptModifier(ILogger<SubscriptModifier> logger) : SingleInstructionModifiers(logger) {
    public const string Name = "subscript";
    
    public override string IconName { get; } = "subscript";
    public override string ModifierName { get; } = Name;
    protected override string Instruction { get; } = "~";
}
