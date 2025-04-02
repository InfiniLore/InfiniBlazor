// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Blazor.Contracts.Markdown;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[KeyedInjectableService<IMultiLineSectionParser>("horizontalRule", ServiceLifetime.Singleton)]
public class HorizontalRuleSectionParser : IMultiLineSectionParser {
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer) {
        writer.Write("<hr>");
    }
}
