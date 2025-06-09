// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using CodeOfChaos.GeneratorTools;

namespace InfiniLore.InfiniBlazor.SourceGenerators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator(LanguageNames.CSharp)]
public class ThemeSymbolsGenerator : IIncrementalGenerator {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValueProvider<ImmutableArray<ThemeSymbolsDto>> data = context.SyntaxProvider
            .CreateSyntaxProvider(Predicate, Transform)
            .Where(dto => dto is not null)
            .Collect()!;

        context.RegisterSourceOutput(context.CompilationProvider.Combine(data), GenerateSources);
    }

    private static bool Predicate(SyntaxNode node, CancellationToken ct) {
        return node switch {
            ClassDeclarationSyntax classDeclaration => classDeclaration.AttributeLists.Count > 0,
            RecordDeclarationSyntax recordDeclaration => recordDeclaration.AttributeLists.Count > 0,
            _ => false
        };

    }

    private static ThemeSymbolsDto? Transform(GeneratorSyntaxContext context, CancellationToken ct) {
        SemanticModel semanticModel = context.SemanticModel;

        INamedTypeSymbol? symbol = context.Node switch {
            ClassDeclarationSyntax classDeclaration => semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol,
            RecordDeclarationSyntax recordDeclaration => semanticModel.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol,
            _ => null
        };
        if (symbol is null) return null;

        // Check if it has the GenerateThemeSymbols attribute
        if (!symbol.HasAttributeWithDisplayName(TypeNames.GenerateThemeSymbolsAttribute)) return null;

        // Check if it implements ITheme
        bool implementsITheme = symbol.AllInterfaces
            .Any(static i => i.ToDisplayString() == TypeNames.IThemeInterface);

        return implementsITheme
            ? new ThemeSymbolsDto(symbol)
            : null;
    }

    private static void GenerateSources(SourceProductionContext context, (Compilation Compilation, ImmutableArray<ThemeSymbolsDto> Data) tuple) {
        (Compilation compilation, ImmutableArray<ThemeSymbolsDto> data) = tuple;
        
        var builder = new GeneratorStringBuilder();
        foreach (ThemeSymbolsDto dto in data) {
            ThemeSymbolsDtoWriter.WritePartialClass(dto, builder, compilation);
            context.AddSource($"{dto.ClassName}.g.cs", builder.ToStringAndClear());

            // ReSharper disable once InvertIf
            if (dto.GenerateVariableStorage) {
                ThemeSymbolsDtoWriter.WriteVariableNames(dto, builder, compilation);
                context.AddSource($"{dto.ClassName}VariableNames.g.cs", builder.ToStringAndClear());
            }
        }
        
    }
}
