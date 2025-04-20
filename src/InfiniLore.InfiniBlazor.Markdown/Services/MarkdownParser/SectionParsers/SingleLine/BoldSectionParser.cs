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
[InjectableSingleton<ISectionHandler>("bold")]
public class BoldSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupNames) : ISectionHandler {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public ParserOrigin SkipOnOrigin => ParserOrigin.Bold;
    
    private readonly int BId = groupNames.GetSingleLineGroupId("b");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        IMdNode boldNode = currentNode.AddChildNode(MdElement.Bold);
        parser.AddSingleLineMatchesToStack(boldValue, boldNode, origin  | SkipOnOrigin);
    }
}
