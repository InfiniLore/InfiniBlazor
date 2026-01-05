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
public partial class TemplateSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"(?<template>(?<!\])(?<open>\{)+(?<t>[^\s{}]+)(?<-open>\})+(?(open)(?!)))", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int TemplateId = Syntax.GroupNumberFromName(MdRegexGroupNames.Template);
    private static readonly int TemplateContentId = Syntax.GroupNumberFromName(MdRegexGroupNames.TemplateContent);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[TemplateContentId].TryGetValue(out string? variableContent)) return;
        if (!match.Groups[TemplateId].TryGetLength(out int variableLength)) return;

        TemplateMdSyntaxNode node = MdSyntaxNodePool<TemplateMdSyntaxNode>.Shared.Get();
        node.WithContent(variableContent)
            .WithBracesCount((variableLength - variableContent.Length) / 2);
        parentNode.AddChildNode(node);
    }
}
