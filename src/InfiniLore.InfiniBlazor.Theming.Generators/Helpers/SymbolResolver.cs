// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// Simple wrapper to make testing not a living hell.
public class SymbolResolver(SemanticModel model) {
    public ISymbol? ResolveSymbol<TSyntaxNode>(TSyntaxNode node) where TSyntaxNode : SyntaxNode => model.GetSymbolInfo(node).Symbol;

    public bool TryResolveSymbol<TSyntaxNode>(TSyntaxNode node, [NotNullWhen(true)] out ISymbol? symbol) where TSyntaxNode : SyntaxNode {
        symbol = ResolveSymbol(node);
        return symbol != null;
    }

    public bool TryResolveSymbol<TSyntaxNode, TSymbol>(TSyntaxNode node, [NotNullWhen(true)] out TSymbol? symbol) where TSyntaxNode : SyntaxNode where TSymbol : class, ISymbol {
        ISymbol? resolved = ResolveSymbol(node);
        symbol = resolved as TSymbol;
        return symbol != null;
    }
}
