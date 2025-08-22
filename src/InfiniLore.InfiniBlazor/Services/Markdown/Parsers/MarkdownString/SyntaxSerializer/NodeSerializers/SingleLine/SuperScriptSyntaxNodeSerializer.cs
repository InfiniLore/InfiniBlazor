// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.SupScript)]
public sealed class SuperScriptSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int SpId = MdRegexLib.GetGroupId(MdRegexGroupNames.SuperScriptContent);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.SuperScript;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownStringMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MarkdownStringMdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return ;
        
        SuperScriptMdSyntaxNode node = SuperScriptMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(superValue, node, parentOrigin | SkipOnOrigin);
    }
}
