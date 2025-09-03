// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;

using MdSyntaxSerializer = Action<IMdSyntaxFragmentStack, IMdSyntaxNode, Match>;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdStringMdSyntaxSerializer>]
public sealed class MdStringMdSyntaxSerializer(ILogger<MdStringMdSyntaxSerializer> logger) : IMdStringMdSyntaxSerializer {
    private readonly FrozenDictionary<string, MdSyntaxSerializer> _elementHandlers = new Dictionary<string, MdSyntaxSerializer> {
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
        [MdRegexGroupNames.HtmlBody] = HtmlBodySyntaxNodeSerializer.Serialize,
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
        [MdRegexGroupNames.Variable] = VariableContentSyntaxNodeSerializer.Serialize,
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

        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            runningSerializer.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode);

            while (runningSerializer.TryPopDto(out MdSyntaxFragment fragment)) {
                switch (fragment) {
                    // Not yet processed
                    case { ParentNode: {} parentNode, Match: {} match }: {
                        ProcessMatch(match, parentNode, runningSerializer);
                        break;
                    }
                    // Already processed and simply needs to be added in correct location
                    case {ParentNode: {} parentNode, ChildNode: {} childNode} : {
                        parentNode.AddChildNode(childNode);
                        break;
                    }
                    
                    // Unhandled state which should never happen
                    default: {
                        logger.Error("Unhandled state in MarkdownStringMdSyntaxSerializer.SerializeToTree with fragment '{fragment}'.", fragment);
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

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, IMdSyntaxFragmentStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success || !_elementHandlers.TryGetValue(group.Name, out MdSyntaxSerializer? handler)) continue;
            handler(runningParser, parentNode, match);
        }
    }
}
