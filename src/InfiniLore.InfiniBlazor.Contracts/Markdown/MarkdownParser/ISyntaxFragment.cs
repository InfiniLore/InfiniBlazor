// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ISyntaxFragment {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AsUnhandledMatch(Match match, IMdSyntaxNode parentNode, HandlerOrigin origin);
    void AsProcessedNode(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);

    bool TryGetAsMatch([NotNullWhen(true)] out Match? match, [NotNullWhen(true)] out IMdSyntaxNode? parentNode, out HandlerOrigin origin);
    bool TryGetAsProcessedNode([NotNullWhen(true)] out IMdSyntaxNode? node, out IMdSyntaxNode? childNode);
}
