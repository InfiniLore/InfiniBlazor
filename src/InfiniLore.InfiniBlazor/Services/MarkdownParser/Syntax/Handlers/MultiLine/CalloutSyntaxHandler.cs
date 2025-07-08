// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.Callout)]
public sealed class CalloutSyntaxHandler : IMdSyntaxHandler {
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    private static readonly int CalloutId = MdRegexLib.GetGroupId(MdRegexGroupNames.Callout);
    private static readonly int CalloutTypeId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutType);
    private static readonly int CalloutModId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutMod);
    private static readonly int CalloutTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutTitle);
    private static readonly int CalloutBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutBody);
    
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        
    }
}
