// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record AutoDocumentedData(
    string Id,
    string? Body
) {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region TryGetAutoDocumentData
    // YES, these look like duplicate code, but due to unique implementation of "WithAttributeLists" being from two different base classes, this is required
    public static bool TryGetFromMember<TMember>(TMember member, [NotNullWhen(true)] out AutoDocumentedData? data) 
        where TMember : MemberDeclarationSyntax
    {
        data = null;
        string? attr = GetAutoDocumentId(member.AttributeLists);
        if (attr == null) return false;

        MemberDeclarationSyntax cleanedMember = member.WithAttributeLists(
            RemoveAutoDocumentAttribute(member.AttributeLists)
        );

        data = new AutoDocumentedData(attr, $"\n    {cleanedMember.ToFullString()}");
        return true;
    }

    public static bool TryGetFromStatement<TStatement>(TStatement member, [NotNullWhen(true)] out AutoDocumentedData? data)
        where TStatement : StatementSyntax 
    {
        data = null;
        string? attr = GetAutoDocumentId(member.AttributeLists);
        if (attr == null) return false;

        StatementSyntax cleanedMember = member.WithAttributeLists(
            RemoveAutoDocumentAttribute(member.AttributeLists)
        );

        data = new AutoDocumentedData(attr, $"\n    {cleanedMember.ToFullString()}");
        return true;
    }

    public static bool TryGetFromSyntaxNode<TSyntaxNode>(TSyntaxNode syntaxNode, [NotNullWhen(true)] out AutoDocumentedData? data)
        where TSyntaxNode : SyntaxNode {
        data = null;
        return syntaxNode switch {
            MemberDeclarationSyntax memberDeclaration => TryGetFromMember(memberDeclaration, out data),
            LocalDeclarationStatementSyntax localDeclaration => TryGetFromStatement(localDeclaration, out data),
            _ => false
        };
    }

    private static SyntaxList<AttributeListSyntax> RemoveAutoDocumentAttribute(SyntaxList<AttributeListSyntax> attributeLists)
        => SyntaxFactory.List(attributeLists
                .Select(attrList => SyntaxFactory.AttributeList(
                    SyntaxFactory.SeparatedList(attrList.Attributes.Where(attr => {
                        string attrName = attr.Name.ToString();
                        return !(attrName.EndsWith("AutoDocument") || attrName.EndsWith("AutoDocumentAttribute"));
                    }))
                ))
                .Where(attrList => attrList.Attributes.Any())// Filter out empty attribute lists
        );

    private static string? GetAutoDocumentId(SyntaxList<AttributeListSyntax> attrLists) {
        return attrLists.SelectMany(attrList => attrList.Attributes)
            .Where(attr => {
                string attrName = attr.Name.ToString();
                return attrName.EndsWith("AutoDocument") || attrName.EndsWith("AutoDocumentAttribute");
            })
            .Select(attr => attr.ArgumentList?.Arguments.FirstOrDefault()?.Expression.ToString().Trim('"'))
            .FirstOrDefault();
    }
    #endregion
}
