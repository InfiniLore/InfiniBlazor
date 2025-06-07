// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class SimpleSyntaxTreeConverter {
    protected virtual FrozenDictionary<MarkdownElement, HtmlTag> MdElementLookup { get; } = new Dictionary<MarkdownElement, HtmlTag> {
        { MarkdownElement.Blockquote, HtmlTag.Create("blockquote") },
        { MarkdownElement.Bold, HtmlTag.Create("strong") },
        { MarkdownElement.CheckboxSelected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled checked/") },
        { MarkdownElement.CheckboxUnselected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled/") },
        { MarkdownElement.CodeBlock, new HtmlTag("<pre><code", "</code></pre>") },
        { MarkdownElement.CodeInline, HtmlTag.Create("code") },
        { MarkdownElement.H1, HtmlTag.Create("h1") },
        { MarkdownElement.H2, HtmlTag.Create("h2") },
        { MarkdownElement.H3, HtmlTag.Create("h3") },
        { MarkdownElement.H4, HtmlTag.Create("h4") },
        { MarkdownElement.H5, HtmlTag.Create("h5") },
        { MarkdownElement.H6, HtmlTag.Create("h6") },
        { MarkdownElement.HorizontalRule, HtmlTag.CreateVoid("hr") },
        { MarkdownElement.Image, HtmlTag.CreateVoid("img") },
        { MarkdownElement.Italic, HtmlTag.Create("em") },
        { MarkdownElement.Link, HtmlTag.Create("a") },
        { MarkdownElement.ListItem, HtmlTag.Create("li") },
        { MarkdownElement.ListOrdered, HtmlTag.Create("ol") },
        { MarkdownElement.ListUnordered, HtmlTag.Create("ul") },
        { MarkdownElement.Paragraph, HtmlTag.Create("p") },
        { MarkdownElement.Strikethrough, HtmlTag.Create("s") },
        { MarkdownElement.Subscript, HtmlTag.Create("sub") },
        { MarkdownElement.Superscript, HtmlTag.Create("sup") },
        { MarkdownElement.Table, HtmlTag.Create("table") },
        { MarkdownElement.TableBody, HtmlTag.Create("tbody") },
        { MarkdownElement.TableCell, HtmlTag.Create("td") },
        { MarkdownElement.TableHead, HtmlTag.Create("thead") },
        { MarkdownElement.TableHeadCell, HtmlTag.Create("th") },
        { MarkdownElement.TableRow, HtmlTag.Create("tr") },
        { MarkdownElement.Tag, HtmlTag.CreateWithClass("span", "tag") },
        { MarkdownElement.Underline, HtmlTag.CreateWithStyle("span", "text-decoration: underline;") }
    }.ToFrozenDictionary();

    private FrozenSet<MarkdownElement> DefinedElements => MdElementLookup.Keys.ToFrozenSet();

    protected delegate void WriteDelegate<in T>(T writer, ReadOnlySpan<char> content);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected ValueTask ProcessNodeTree<T>(IMarkdownSyntaxTree tree, T writer, WriteDelegate<T> writeContent, CancellationToken ct = default) {
        Dictionary<int, MarkdownElement> depthCache = MarkdownPoolCache.DepthCachePool.Get();
        try {
            int lastKnownDepth = -1;

            foreach (IMarkdownSyntaxVisitor mdNodeVisitor in tree.VisitNodesBreadthFirst()) {
                int depth = mdNodeVisitor.Depth;
                IMarkdownSyntaxNode node = mdNodeVisitor.Node;

                if (lastKnownDepth + 1 > depth) 
                    CloseOpenTags(writer, depthCache, depth, writeContent);

                MarkdownElement element = node.Element;
                if (element is MarkdownElement.Content or MarkdownElement.HtmlContent) {
                    ReadOnlySpan<char> content = node.Content;
                    if (content.Length == 0) continue;

                    writeContent(writer, content);
                    continue;
                }

                if (!DefinedElements.Contains(element)) continue;

                HtmlTag htmlTag = MdElementLookup[element];
                writeContent(writer, htmlTag.OpenTagSpan);

                WriteAttributes(writer,  writeContent, node);

                if (!htmlTag.HasClosingSmallerThan) writeContent(writer, ">");

                if (htmlTag.HasClosingTag) depthCache.AddOrUpdate(depth, element);
                lastKnownDepth = depth;
            }

            CloseOpenTags(writer, depthCache, -1, writeContent);
            return ValueTask.CompletedTask;
        }
        finally {
            MarkdownPoolCache.DepthCachePool.Return(depthCache);
        }
    }
    private static void WriteAttributes<T>(T writer, WriteDelegate<T> writeContent, IMarkdownSyntaxNode node) {
        if (!node.TryGetAttributesSpan(out int count, out ReadOnlySpan<MarkdownAttribute> attributes, out ReadOnlySpan<string> source)) return;
        for (int i = 0; i < count; i++) {
            writeContent(writer, attributes[i] switch {
                MarkdownAttribute.CodeLanguage => " class=\"language-",
                MarkdownAttribute.LinkHref => " href=\"",
                MarkdownAttribute.ImageTitle => " title=\"",
                MarkdownAttribute.ImageSource => " src=\"",
                MarkdownAttribute.ImageAlt => " alt=\"",
                _ =>  throw new NotImplementedException()
            });
            writeContent(writer, source[i]);
            writeContent(writer, "\"");
        }
    }

    private void CloseOpenTags<T>(T writer, Dictionary<int, MarkdownElement> depthCache, int depth, WriteDelegate<T> writeContent) {
        if (depthCache.Count == 0) return;

        Span<int> keysToRemove = stackalloc int[depthCache.Count];
        int count = 0;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (int k in depthCache.Keys) {
            if (k < depth) continue;
            keysToRemove[count++] = k;
        }

        Span<int> slice = keysToRemove[..count];
        slice.Sort();
        for (int i = count - 1; i >= 0; i--) {
            int key = slice[i];
            MarkdownElement closingEl = depthCache[key];
            HtmlTag htmlTag = MdElementLookup[closingEl];
            writeContent(writer, htmlTag.CloseTagSpan);
            depthCache.Remove(key);
        }
    }
}
