// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Pools;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("codeBlock")]
public class CodeBlockSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int CBodyId = groupNames.GetMultiLineGroupId("cBody");
    private readonly int CLangId = groupNames.GetMultiLineGroupId("cLang");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        IMdNode preNode = currentNode.AddChild(MdElement.Pre);
        IMdNode codeNode = preNode.AddChild(MdElement.Code);
        
        ReadOnlySpan<char> langNameValue = entireMatch.Groups[CLangId].ValueSpan;
        if (!langNameValue.IsEmpty) {
            codeNode.WithClass($"language-{langNameValue}");
        }
        
        string content = ProcessCodeBlockContent(ref codeBlockBody);
        parser.AddStringToStack(content, codeNode, origin | ParserOrigin.PreserveHtml);
    }

    private static string ProcessCodeBlockContent(ref ReadOnlySpan<char> content) {
        int currentIndex = 0;
        StringBuilder sb = StringBuilderPool.Get();
        try {
            foreach (ValueMatch match in HtmlSymbolLookup.CodeBlockRegex.EnumerateMatches(content)) {
                int matchIndex = match.Index;
                int matchLength = match.Length;
                if (currentIndex < matchIndex) {
                    sb.Append(content.Slice(currentIndex, matchIndex - currentIndex));
                }

                if (HtmlSymbolLookup.CodeBlockAlternateLookup.TryGetValue(content.Slice(matchIndex, matchLength), out string? replacement)) {
                    sb.Append(replacement.AsSpan());
                }

                currentIndex = matchIndex + matchLength;
            }

            if (currentIndex < content.Length) {
                sb.Append(content[currentIndex..]);
            }
            return sb.ToString();
        }
        finally {
            StringBuilderPool.Return(sb);
        }

        
    }
}
