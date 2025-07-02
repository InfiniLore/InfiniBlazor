// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<MarkupString>>]
public class ToStyledStringConverter(IMarkdownSyntaxTreeConverter<string> stringConverter) : IMarkdownSyntaxTreeConverter<MarkupString> {
    public MarkupString Convert(IMarkdownSyntaxTree tree) 
        => new(stringConverter.Convert(tree));
}

