// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMultiLineSectionParser {
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin);
}
