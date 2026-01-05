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
public partial class FrontmatterSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"(?<frontmatter>(?<open>^-{3,}) *(?<fLang>.+)?\r?\n(?<fBody>[\s\S]*?)\r?\n\k<open>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    internal static partial Regex Syntax { get; }
    
    private static readonly int LangId = Syntax.GroupNumberFromName(MdRegexGroupNames.FrontmatterLang);
    private static readonly int BodyId = Syntax.GroupNumberFromName(MdRegexGroupNames.FrontmatterBody);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        FrontMatterMdSyntaxNode node = MdSyntaxNodePool<FrontMatterMdSyntaxNode>.Shared.Get();
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
