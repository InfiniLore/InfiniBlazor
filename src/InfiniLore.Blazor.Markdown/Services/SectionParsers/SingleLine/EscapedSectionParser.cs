// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[KeyedInjectableService<ISingleLineSectionParser>("escaped", ServiceLifetime.Singleton)]
public class EscapedSectionParser(IValueChangerLookupService lookupService) : ISingleLineSectionParser {
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        char value = group.ValueSpan[1];
        ReadOnlySpan<char> span = [value];
        if (lookupService.AlternateLookup.TryGetValue(span, out string? alternate)) {
            writer.Write(alternate);
            return;
        }
        
        writer.Write(value);
    }
}
