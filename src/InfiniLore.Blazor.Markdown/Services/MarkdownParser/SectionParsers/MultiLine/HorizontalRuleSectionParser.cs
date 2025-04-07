// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("horizontalRule")]
public class HorizontalRuleSectionParser : IMultiLineSectionParser {
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer) {
        writer.Write("<hr>");
    }
}
