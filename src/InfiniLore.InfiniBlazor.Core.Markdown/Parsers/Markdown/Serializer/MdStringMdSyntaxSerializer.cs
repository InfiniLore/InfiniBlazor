// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdStringMdSyntaxSerializer>]
public sealed class MdStringMdSyntaxSerializer(ILogger<MdStringMdSyntaxSerializer> logger) : IMdStringMdSyntaxSerializer {
    private delegate void MdSyntaxSerializerAction(IMdSyntaxFragmentStack stack, IMdSyntaxNode node, Match match);

    private readonly FrozenDictionary<string, MdSyntaxSerializerAction> _elementHandlers = new Dictionary<string, MdSyntaxSerializerAction> {
        [MdRegexGroupNames.BlockQuote] = BlockQuoteSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Bold] = BoldSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Callout] = CalloutSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.CodeBlock] = CodeBlockSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.CodeInline] = CodeInlineSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Emote] = EmoteSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Escaped] = EscapedSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.HeadingSimple] = HeadingSimpleSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Heading] = HeadingSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.HorizontalRule] = HorizontalRuleSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.HtmlBody] = HtmlSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Italic] = ItalicSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Link] = LinkSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.List] = ListSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.NewLine] = NewLineSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Paragraph] = ParagraphSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Strike] = StrikeSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.SubScript] = SubScriptSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.SuperScript] = SuperScriptSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Table] = TableSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Tag] = TagSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Underline] = UnderlineSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.User] = UserSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.WikiLink] = WikiLinkSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Template] = TemplateSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.FootnoteReference] = FootnoteReferenceSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.FootnoteDescription] = FootnoteDescriptionSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Highlight] = HighlightSyntaxNodeSerializer.Serialize,
        [MdRegexGroupNames.Wrapper] = WrapperSyntaxNodeSerializer.Serialize,
    }.ToFrozenDictionary();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToTree(string markdown) {
        MdSyntaxTree nodeTree = MdSyntaxTree.Pool.Get();
        SerializeToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void SerializeToTree(string markdown, IMdSyntaxTree nodeTree) {
        MdSyntaxFragmentStack runningSerializer = MdSyntaxFragmentStack.Pool.Get();
        runningSerializer.TreeReference = nodeTree;

        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            TryExtractFrontMatter(normalized, nodeTree, out int newStartAtIndex);
            runningSerializer.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode, newStartAtIndex);

            while (runningSerializer.TryPopDto(out MdSyntaxFragment fragment)) {
                switch (fragment) {
                    // Not yet processed
                    case { ParentNode: {} parentNode, Match: {} match }: {
                        ProcessMatch(match, parentNode, runningSerializer);
                        break;
                    }

                    // Already processed and simply needs to be added in correct location
                    case { ParentNode: {} parentNode, ChildNode: {} childNode }: {
                        parentNode.AddChildNode(childNode);
                        break;
                    }

                    // Unhandled state which should never happen
                    default: {
                        logger.Error("Unhandled state in MarkdownMdSyntaxSerializer.SerializeToTree with fragment '{fragment}'.", fragment);
                        break;
                    }
                }
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing Markdown, during tree conversion.");
            throw;
        }
        finally {
            MdSyntaxFragmentStack.Pool.Return(runningSerializer);
        }
    }

    // TODO needs to be optional
    private static void TryExtractFrontMatter(string markdown, IMdSyntaxTree nodeTree, out int newStartAtIndex) {
        newStartAtIndex = 0;
        Match match = MdRegexLib.FindFrontmatterRegex.Match(markdown);
        if (!match.Success) return;

        newStartAtIndex = match.Index + match.Length;

        FrontMatterMdSyntaxNode node = FrontMatterMdSyntaxNode.Pool.Get();
        if (match.Groups[MdRegexGroupNames.FrontmatterLang].TryGetValue(out string? lang)) {
            node.WithContent(lang);
        }
        if (match.Groups[MdRegexGroupNames.FrontmatterBody].TryGetValue(out string? body)) {
            node.WithContent(body);
        }
        
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
        
        nodeTree.RootNode.AddChildNode(node);
    }

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, IMdSyntaxFragmentStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success || !_elementHandlers.TryGetValue(group.Name, out MdSyntaxSerializerAction? handler)) continue;

            handler(runningParser, parentNode, match);
        }
    }
}
