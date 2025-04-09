// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.Blazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("code-inline")]
public class CodeInlineModifier(ILogger<CodeInlineModifier> logger) : SingleInstructionModifiers(logger) {
    public override string IconName => "code";
    public override string ModifierName => "code-inline";
    protected override string Instruction => "`";
}
