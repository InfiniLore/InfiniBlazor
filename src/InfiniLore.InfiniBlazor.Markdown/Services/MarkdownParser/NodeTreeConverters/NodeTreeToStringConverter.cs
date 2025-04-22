// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Pools;
using System.Buffers;
using System.Collections.Frozen;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NodeTreeToStringConverter : IMdNodeTreeConverter<string> {
    private static readonly FrozenDictionary<MdElement, (string, string)> MdElementLookup = new Dictionary<MdElement, (string, string)>() {
        {MdElement.Blockquote, ("<blockquote","</blockquote>")},
        {MdElement.Bold, ("<strong","</strong>")},
        {MdElement.CheckboxSelected, ("<input type=\"checkbox\" disabled checked /",string.Empty)},
        {MdElement.CheckboxUnselected, ("<input type=\"checkbox\" disabled /",string.Empty)},
        {MdElement.CodeBlock, ("<pre><code ","</code></pre>")},
        {MdElement.CodeInline, ("<code","</code>")},
        {MdElement.H1, ("<h1","</h1>")},
        {MdElement.H2, ("<h2","</h2>")},
        {MdElement.H3, ("<h3","</h3>")},
        {MdElement.H4, ("<h4","</h4>")},
        {MdElement.H5, ("<h5","</h5>")},
        {MdElement.H6, ("<h6","</h6>")},
        {MdElement.HorizontalRule, ("<hr",string.Empty)},
        {MdElement.Image, ("<img",string.Empty)},
        {MdElement.Italic, ("<em","</em>")},
        {MdElement.Link, ("<a","</a>")},
        {MdElement.ListItem, ("<li","</li>")},
        {MdElement.ListOrdered, ("<ol","</ol>")},
        {MdElement.ListUnordered, ("<ul","</ul>")},
        {MdElement.Paragraph, ("<p","</p>")},
        {MdElement.Strikethrough, ("<s","</s>")},
        {MdElement.Subscript, ("<sub","</sub>")},
        {MdElement.Superscript, ("<sup","</sup>")},
        {MdElement.Table, ("<table","</table>")},
        {MdElement.TableBody, ("<tbody","</tbody>")},
        {MdElement.TableCell, ("<td","</td>")},
        {MdElement.TableHead, ("<thead","</thead>")},
        {MdElement.TableHeadCell, ("<th","</th>")},
        {MdElement.TableRow, ("<tr","</tr>")},
        {MdElement.Tag, ("<span class=\"tag\"","</span>")},
        {MdElement.Underline, ("<span style=\"text-decoration: underline;\"","</span>")},
    }.ToFrozenDictionary();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Convert(IMdNodeTree tree) {
        StringBuilder builder = StringBuilderPool.Get();
        Dictionary<int, MdElement> depthCache = DepthCachePool.Get();
        try {
            int lastKnownDepth = -1;
            
            foreach (IMdNodeVisitor mdNodeVisitor in tree) {
                int depth = mdNodeVisitor.Depth;
                IMdNode node = mdNodeVisitor.Node;

                // write the end tag of a previous sibling
                if (lastKnownDepth >= depth) {
                    CloseOpenTags(builder, depthCache, depth);
                }
                
                // write the current html tag
                MdElement element = node.Element;
                switch (element) {
                    case MdElement.Content :
                    case MdElement.HtmlContent : {
                        builder.Append(node.Content.AsSpan());
                        break;    
                    }

                    // Elements with Children
                    case MdElement.CodeBlock: 
                    case MdElement.Blockquote :
                    case MdElement.Bold :
                    case MdElement.CheckboxSelected :
                    case MdElement.CheckboxUnselected :
                    case MdElement.CodeInline :
                    case MdElement.H1 :
                    case MdElement.H2 :
                    case MdElement.H3 :
                    case MdElement.H4 :
                    case MdElement.H5 :
                    case MdElement.H6 :
                    case MdElement.HorizontalRule :
                    case MdElement.Image :
                    case MdElement.Italic :
                    case MdElement.Link :
                    case MdElement.ListItem :
                    case MdElement.ListOrdered :
                    case MdElement.ListUnordered :
                    case MdElement.Paragraph :
                    case MdElement.Strikethrough :
                    case MdElement.Subscript :
                    case MdElement.Superscript :
                    case MdElement.Table :
                    case MdElement.TableBody :
                    case MdElement.TableCell :
                    case MdElement.TableHead :
                    case MdElement.TableHeadCell :
                    case MdElement.TableRow :
                    case MdElement.Tag :
                    case MdElement.Underline : {
                        (string tagOpen, string tagClose) = MdElementLookup[element];
                        ReadOnlySpan<char> tagOpenSpan = tagOpen.AsSpan();
                        ReadOnlySpan<char> tagCloseSpan = tagClose.AsSpan();
                        
                        builder.Append(tagOpenSpan);
                        if (node.Classes.Count > 0) {
                            builder.Append(" class=\"".AsSpan());
                            bool isFirst = true;
                            foreach (ReadOnlySpan<char> cssClass in node.Classes) {
                                if(!isFirst) builder.Append(' ');
                                builder.Append(cssClass);
                                isFirst = false;
                            }
                            builder.Append('"');   
                        }
                        if (node.Attributes.Count > 0) {
                            builder.Append(' ');
                            foreach (KeyValuePair<string, string> attribute in node.Attributes) {
                                ReadOnlySpan<char> keySpan = attribute.Key.AsSpan();
                                ReadOnlySpan<char> valueSpan = attribute.Value.AsSpan();
                                
                                builder.Append(keySpan);
                                builder.Append("=\"".AsSpan());
                                builder.Append(valueSpan);
                                builder.Append('"');
                            }
                        }
                        builder.Append('>');
                        
                        if (tagCloseSpan.Length != 0) depthCache.AddOrUpdate(depth, element);
                        lastKnownDepth = depth;
                        break;  
                    }

                    case MdElement.Undefined:
                    default: break;
                }
            }
            
            // write the end tags of all remaining siblings
            CloseOpenTags(builder, depthCache, -1);
            
            // write the data
            return builder.ToString();
        }
        finally {
            StringBuilderPool.Return(builder);
            DepthCachePool.Return(depthCache);
        }
    }
    private static void CloseOpenTags(StringBuilder builder, Dictionary<int, MdElement> depthCache, int depth) {
        int[] oldKeysArr = ArrayPool<int>.Shared.Rent(depthCache.Count);
        int count = 0;
                    
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (int k in depthCache.Keys) {
            if (k < depth) continue;
            oldKeysArr[count++] = k;
        }

        Array.Sort(oldKeysArr, 0, count);
        Array.Reverse(oldKeysArr,0, count);
                    
        for (int i = 0; i < count; i++) {
            int key = oldKeysArr[i];
            MdElement closingEl = depthCache[key];
            (string _, string closingTag) = MdElementLookup[closingEl];
            ReadOnlySpan<char> closingTagSpan = closingTag.AsSpan();
            builder.Append(closingTagSpan);
            depthCache.Remove(key);
        }
                    
        ArrayPool<int>.Shared.Return(oldKeysArr);
    }
}
