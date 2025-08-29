// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniLore.InfiniBlazor.AutoDocumenting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IAutoDocumenter>]
[SuppressMessage("Usage", "BL0006:Do not use RenderTree types")]// Yes, I am aware that this isn't perfect as the type names are expected to change at some point. For now, it works.
public class AutoDocumenter(IAttributeValueConverter attributeValueConverter) : IAutoDocumenter {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string ConvertToString(RenderFragment fragment) {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (fragment == null) return string.Empty;

        // Render the RenderFragment into a RenderTreeBuilder
        var builder = new RenderTreeBuilder();
        fragment.Invoke(builder);

        // Retrieve the frames and process them into a formatted string
        ArrayRange<RenderTreeFrame> frames = builder.GetFrames();
        return ProcessFrames(frames);
    }

    private string ProcessFrames(ArrayRange<RenderTreeFrame> frames) {
        var sb = new StringBuilder();
        ProcessFrames(frames, 0, sb, 0, frames.Count);

        // ReSharper disable once InvertIf
        if (sb.Length > 0) {
            if (sb[^1] == '\n') sb.Length--;
            if (sb[^1] == '\r') sb.Length--;
        }

        return sb.ToString();
    }

    private int ProcessFrames(ArrayRange<RenderTreeFrame> frames, int startIndex, StringBuilder sb, int depth, int limit) {
        int framesProcessed = 0;
        bool isChildContentRendered = false;// Tracks if a RenderFragment (e.g., ChildContent) was rendered

        for (int i = startIndex; framesProcessed < limit; i++, framesProcessed++) {
            RenderTreeFrame frame = frames.Array[i];
            switch (frame.FrameType) {
                case RenderTreeFrameType.Element: {
                    // Open the element
                    sb.Append(new string(' ', depth * 2).TrimEnd());
                    sb.Append($"<{frame.ElementName.Trim()}>");

                    // Process its attributes
                    i = ProcessAttributes(frames, i + 1, sb, depth, ref isChildContentRendered);

                    if (frame.ElementSubtreeLength > 0) {
                        sb.AppendLine();
                        depth++;
                        i = ProcessFrames(frames, i + 1, sb, depth, frame.ElementSubtreeLength - 1);
                        depth--;
                        sb.Append(new string(' ', depth * 2).TrimEnd());
                    }

                    // Close the tag
                    sb.AppendLine($"</{frame.ElementName.Trim()}>");
                    break;
                }

                case RenderTreeFrameType.Text: {
                    // Render inline text after trimming unnecessary whitespace
                    string textContent = frame.TextContent.Trim();// Remove extra spaces
                    if (!string.IsNullOrEmpty(textContent))// Skip empty text
                    {
                        sb.Append(new string(' ', depth * 2));// Proper indentation for text
                        sb.Append(textContent);
                    }

                    break;
                }

                case RenderTreeFrameType.Component: {
                    // Open the tag for the component
                    sb.Append(new string(' ', depth * 2));
                    sb.Append($"<{frame.ComponentType.Name.Trim()}");

                    // Check for attributes or RenderFragment (ChildContent)
                    i = ProcessAttributes(frames, i + 1, sb, depth, ref isChildContentRendered);

                    if (isChildContentRendered) {
                        sb.Append($"</{frame.ComponentType.Name.Trim()}>");
                    }
                    else if (frame.ComponentSubtreeLength > 0) {
                        sb.Append('>');
                        depth++;
                        i = ProcessFrames(frames, i + 1, sb, depth, frame.ComponentSubtreeLength - 1);
                        depth--;
                        sb.Append(new string(' ', depth * 2));
                        sb.Append($"</{frame.ComponentType.Name.Trim()}>");
                        sb.AppendLine();
                    }
                    // No child content and no subtree - this is an empty component
                    else {
                        sb.Append($"></{frame.ComponentType.Name.Trim()}>");
                        if (depth > 0 || framesProcessed < limit - 1) {
                            sb.AppendLine();
                        }
                    }

                    break;
                }

                case RenderTreeFrameType.Markup: {
                    sb.Append(new string(' ', depth * 2));
                    sb.Append(frame.MarkupContent.Trim());
                    sb.AppendLine();
                    break;
                }

                // Handle recursive processing for regions
                case RenderTreeFrameType.Region: {
                    i = ProcessFrames(frames, i + 1, sb, depth, frame.RegionSubtreeLength);
                    break;
                }

                case RenderTreeFrameType.None:
                case RenderTreeFrameType.Attribute:
                case RenderTreeFrameType.ElementReferenceCapture:
                case RenderTreeFrameType.ComponentReferenceCapture:
                case RenderTreeFrameType.ComponentRenderMode:
                case RenderTreeFrameType.NamedEvent: break;
                default: throw new ArgumentOutOfRangeException(nameof(frames));
            }
        }

        return startIndex + framesProcessed - 1;// Return processed index
    }

    private int ProcessAttributes(ArrayRange<RenderTreeFrame> frames, int currentIndex, StringBuilder sb, int depth, ref bool isChildContentRendered) {
        int originalIndex = currentIndex;

        while (currentIndex < frames.Count) {
            RenderTreeFrame frame = frames.Array[currentIndex];

            // Stop if the frame isn't an attribute
            if (frame.FrameType != RenderTreeFrameType.Attribute) break;

            // Render the RenderFragment (like ChildContent)
            if (frame.AttributeValue is RenderFragment renderFragment) {
                sb.Append('>');
                var childBuilder = new RenderTreeBuilder();
                renderFragment.Invoke(childBuilder);

                // Process the RenderFragment's child frames
                ArrayRange<RenderTreeFrame> childFrames = childBuilder.GetFrames();

                // Check if this is simple text content (single text frame)
                bool isSimpleTextContent = childFrames.Count == 1 
                    && childFrames.Array[0].FrameType == RenderTreeFrameType.Text;
                
                // For simple text content, don't add indentation - render inline
                if (isSimpleTextContent) {
                    string textContent = childFrames.Array[0].TextContent.Trim();
                    if (!string.IsNullOrEmpty(textContent)) {
                        sb.Append(textContent);
                    }
                }
                else {
                    // For complex content, use normal processing with indentation
                    ProcessFrames(childFrames, 0, sb, depth + 1, childFrames.Count);
                }

                isChildContentRendered = true;
            }
            // Render normal attributes inline
            else {
                string attributeValue = attributeValueConverter.FormatAttributeValue(frame);
                sb.Append($" {frame.AttributeName}=\"{attributeValue}\"");
            }

            currentIndex++;
        }

        // Only close the tag if we processed attributes but no child content was rendered
        if (!isChildContentRendered && currentIndex > originalIndex) {
            sb.Append('>');
        }

        return currentIndex - 1;
    }
}
