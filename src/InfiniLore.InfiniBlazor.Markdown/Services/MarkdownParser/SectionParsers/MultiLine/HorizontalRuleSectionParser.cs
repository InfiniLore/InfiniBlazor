// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("horizontalRule")]
public class HorizontalRuleSectionParser : IMultiLineSectionParser {
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        writer.Write("<hr>");
    }
}
