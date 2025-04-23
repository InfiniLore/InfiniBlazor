// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NodeTreeToTextWriterConverter<T> : IMdNodeTreeToWriterConverter<T> where T : TextWriter {
    private readonly FrozenDictionary<MdElement, (string, string)> MdElementLookup = new Dictionary<MdElement, (string, string)> {
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
    public void Convert(IMdNodeTree tree, T writer) {
        var dictionary = new Dictionary<int, MdElement>();
            int lastKnownDepth = -1;
            
            foreach (IMdNodeVisitor nodeVisitor in tree.VisitNodesBreadthFirst()) {
                int depth = nodeVisitor.Depth;
                IMdNode node = nodeVisitor.Node;

                // write the end tag of a previous sibling
                if (lastKnownDepth >= depth) {
                    int[] oldKeys = dictionary.Keys.Where(k => k >= depth).ToArray();
                    Array.Sort(oldKeys);
                    Array.Reverse(oldKeys);
                    foreach (int key in oldKeys) {
                        MdElement closingEl = dictionary[key];
                        (string _, string closingTag) = MdElementLookup[closingEl];
                        writer.Write(closingTag.AsSpan());
                        dictionary.Remove(key);   
                    }
                }
                
                // write the current html tag
                MdElement element = node.Element;
                switch (element) {
                    case MdElement.Content :
                    case MdElement.HtmlContent : {
                        writer.Write(node.Content.AsSpan());
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
                        writer.Write(tagOpen.AsSpan());
                        if (node.Classes.Count > 0) writer.Write($" class=\"{string.Join(' ', node.Classes)}\"");
                        if (node.Attributes.Count > 0) {
                            foreach (KeyValuePair<string, string> attribute in node.Attributes) {
                                writer.Write($" {attribute.Key}=\"{attribute.Value}\"");
                            }
                        }
                        writer.Write('>');
                        
                        if (tagClose.IsNotNullOrEmpty()) dictionary.AddOrUpdate(depth, element);
                        lastKnownDepth = depth;
                        break;  
                    }

                    case MdElement.Undefined:
                    default: break;
                }
                // write the data
            }
            
            // write the end tags of all remaining siblings
            int[] keys = dictionary.Keys.ToArray();
            Array.Sort(keys);
            Array.Reverse(keys);
            foreach (int key in keys) {
                MdElement closingElement = dictionary[key];
                (string _, string closingTag) = MdElementLookup[closingElement];
                writer.Write(closingTag);
            }
            
    }
}
