// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("codeBlock")]
public class CodeBlockSectionParser : ISectionHandler {

    private static readonly int CBodyId = CachedRegexGroupNames.GetMultiLineGroupId("cBody");
    private static readonly int CLangId = CachedRegexGroupNames.GetMultiLineGroupId("cLang");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    private static FrozenDictionary<string, string> CodeBlockLookup { get; } = new Dictionary<string, string> {
        { "\r\n", "\n" },
        { "&", "&amp;" },
        { "<", "&lt;" },
        { ">", "&gt;" }
    }.ToFrozenDictionary();

    private static FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> CodeBlockAlternateLookup { get; } = CodeBlockLookup.GetAlternateLookup<ReadOnlySpan<char>>();
    private static Regex CodeBlockRegex { get; } = new(string.Join('|', CodeBlockLookup.Keys), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.Compiled);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        IMdNode codeNode = currentNode.AddChildNode(MdElement.CodeBlock);

        ReadOnlySpan<char> langNameValue = entireMatch.Groups[CLangId].ValueSpan;
        if (!langNameValue.IsEmpty) codeNode.WithClass($"language-{langNameValue}");

        string content = ProcessCodeBlockContent(ref codeBlockBody);
        codeNode.WithContent(content);
    }

    private static string ProcessCodeBlockContent(ref ReadOnlySpan<char> content) {
        int currentIndex = 0;
        StringBuilder sb = PoolCache.StringBuilderPool.Get();
        try {
            foreach (ValueMatch match in CodeBlockRegex.EnumerateMatches(content)) {
                int matchIndex = match.Index;
                int matchLength = match.Length;
                if (currentIndex < matchIndex) {
                    sb.Append(content.Slice(currentIndex, matchIndex - currentIndex));
                }

                if (CodeBlockAlternateLookup.TryGetValue(content.Slice(matchIndex, matchLength), out string? replacement)) {
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
            PoolCache.StringBuilderPool.Return(sb);
        }


    }
}
