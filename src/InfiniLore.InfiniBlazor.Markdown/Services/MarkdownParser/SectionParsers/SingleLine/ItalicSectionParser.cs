// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("italic")]
public class ItalicSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupNames) : ISectionHandler {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public ParserOrigin SkipOnOrigin => ParserOrigin.Italic;
    
    private readonly int IId = groupNames.GetSingleLineGroupId("i");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[IId].TryGetValue(out string? italicValue)) return;
        
        IMdNode node = currentNode.AddChildNode(MdElement.Italic);
        parser.AddSingleLineMatchesToStack(italicValue, node, origin  | SkipOnOrigin);
    }
}
