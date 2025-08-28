// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Text;

namespace InfiniLore.InfiniBlazor.AutoDocumenting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

// Yes, we are aware that this isn't perfect as the type names are expected to change at some point. For now, it works.
#pragma warning disable BL0006 

[InjectableSingleton<IAutoDocumenter>]
public class AutoDocumenter : IAutoDocumenter {
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
        return sb.ToString();
    }

    private int ProcessFrames(ArrayRange<RenderTreeFrame> frames, int startIndex, StringBuilder sb, int depth, int limit) {
        int framesProcessed = 0;
        bool isChildContentRendered = false;// Tracks if a RenderFragment (e.g., ChildContent) was rendered

        for (int i = startIndex; framesProcessed < limit; i++, framesProcessed++) {
            RenderTreeFrame frame = frames.Array[i];
            switch (frame.FrameType) {
                case RenderTreeFrameType.Element:
                    // Open the element
                    sb.Append(new string(' ', depth * 2).TrimEnd());// Ensure indentation has no trailing spaces
                    sb.Append($"<{frame.ElementName.Trim()}>");// Trim the tag name and content

                    // Process its attributes
                    i = ProcessAttributes(frames, i + 1, sb, depth, ref isChildContentRendered);

                    if (frame.ElementSubtreeLength > 0) {
                        sb.AppendLine();// Add a newline if there are child frames
                        depth++;
                        i = ProcessFrames(frames, i + 1, sb, depth, frame.ElementSubtreeLength - 1);
                        depth--;
                        sb.Append(new string(' ', depth * 2).TrimEnd());// Indentation for closing tag
                    }

                    // Close the tag
                    sb.AppendLine($"</{frame.ElementName.Trim()}>");
                    break;

                case RenderTreeFrameType.Text:
                    // Render inline text after trimming unnecessary whitespace
                    string textContent = frame.TextContent.Trim();// Remove extra spaces
                    if (!string.IsNullOrEmpty(textContent))// Skip empty text
                    {
                        sb.Append(new string(' ', depth * 2));// Proper indentation for text
                        sb.Append(textContent);
                    }

                    break;

                case RenderTreeFrameType.Component:
                    // Open the tag for the component
                    sb.Append(new string(' ', depth * 2));
                    sb.Append($"<{frame.ComponentType.Name.Trim()}>");

                    // Check for attributes or RenderFragment (ChildContent)
                    i = ProcessAttributes(frames, i + 1, sb, depth, ref isChildContentRendered);

                    if (isChildContentRendered) {
                        sb.AppendLine($"</{frame.ComponentType.Name.Trim()}>");
                    }
                    else if (frame.ComponentSubtreeLength > 0) {
                        sb.AppendLine(">");
                        depth++;
                        i = ProcessFrames(frames, i + 1, sb, depth, frame.ComponentSubtreeLength - 1);
                        depth--;
                        sb.Append(new string(' ', depth * 2));
                        sb.AppendLine($"</{frame.ComponentType.Name.Trim()}>");
                    }
                    else {
                        sb.AppendLine(" />");// If no children, self-close
                    }

                    break;

                case RenderTreeFrameType.Markup:
                    sb.Append(new string(' ', depth * 2)); // Properly render indentation
                    sb.Append(frame.MarkupContent.Trim()); // Trim trailing spaces
                    sb.AppendLine(); // Ensure single newline for markup
                    break;

                case RenderTreeFrameType.Region:
                    // Handle recursive processing for regions
                    i = ProcessFrames(frames, i + 1, sb, depth, frame.RegionSubtreeLength);
                    break;

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
        while (currentIndex < frames.Count) {
            RenderTreeFrame frame = frames.Array[currentIndex];

            if (frame.FrameType != RenderTreeFrameType.Attribute)
                break;// Stop if the frame isn't an attribute

            if (frame.AttributeValue is RenderFragment renderFragment) {
                // Render the RenderFragment (like ChildContent)
                sb.Append('>');// Close the parent tag
                var childBuilder = new RenderTreeBuilder();
                renderFragment.Invoke(childBuilder);

                // Process the RenderFragment's child frames
                ArrayRange<RenderTreeFrame> childFrames = childBuilder.GetFrames();
                sb.AppendLine();// Ensure a clean newline for child-rendered content
                ProcessFrames(childFrames, 0, sb, depth + 1, childFrames.Count);// Recursively process child frames
                isChildContentRendered = true;// Mark RenderFragment as handled
            }
            else {
                // Render normal attributes inline
                sb.Append($" {frame.AttributeName}=\"{frame.AttributeValue}\"");
            }

            currentIndex++;
        }

        return currentIndex - 1;// Return the final attribute index
    }
}
#pragma warning restore BL0006
