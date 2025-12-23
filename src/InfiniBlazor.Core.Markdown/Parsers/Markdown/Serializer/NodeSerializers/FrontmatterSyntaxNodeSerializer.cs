// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FrontmatterSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int LangId = MdRegexLib.GetGroupId(MdRegexGroupNames.FrontmatterLang);
    private static readonly int BodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.FrontmatterBody);
    
    public Regex Syntax { get; } = MdRegexLib.FrontmatterRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        FrontMatterMdSyntaxNode node = FrontMatterMdSyntaxNode.Pool.Get();
        if (match.Groups[LangId].TryGetValue(out string? lang)) node.WithLanguage(lang);
        if (match.Groups[BodyId].TryGetValue(out string? body)) node.WithContent(body);
        
        ReadOnlySpan<char> span = match.ValueSpan;
        int dashCount = 0;
        int spaceCount = 0;
        foreach (char t in span) {
            switch (t) {
                case '-':
                    dashCount++;
                    continue;
                case ' ':
                    spaceCount++;
                    continue;
            }
            break;
        }
        
        node.WithDashesCount(dashCount);
        node.WithLeadingSpaces(spaceCount);
        
        parentNode.AddChildNode(node);
    }
}
