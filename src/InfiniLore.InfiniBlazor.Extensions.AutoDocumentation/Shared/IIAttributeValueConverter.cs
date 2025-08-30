// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components.RenderTree;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("Usage", "BL0006:Do not use RenderTree types")]// Yes, I am aware that this isn't perfect as the type names are expected to change at some point. For now, it works.
public interface IAttributeValueConverter {
    string FormatAttributeValue(RenderTreeFrame frame);
}
