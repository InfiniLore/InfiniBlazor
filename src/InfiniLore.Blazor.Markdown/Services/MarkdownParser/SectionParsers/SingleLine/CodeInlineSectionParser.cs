// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISingleLineSectionParser>("code")]
public class CodeInlineSectionParser(ICachedRegexGroupNames groupNames) : ISingleLineSectionParser {
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.Code;

    private readonly int CId = groupNames.GetSingleLineGroupId("c");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        string output = HtmlEncoder.Default.Encode(normalizedBackticks);

        writer.Write("<code>");
        writer.Write(output);
        writer.Write("</code>");
    }
}
