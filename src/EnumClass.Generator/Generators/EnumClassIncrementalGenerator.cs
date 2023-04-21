using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator.Generators;

[Generator]
public class EnumClassIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> enums = 
            generatorContext
               .SyntaxProvider
               .CreateSyntaxProvider(FilterEnumDeclarations, GetSemanticModelForEnumClass)
               .Where(x => x is not null)!;

        var provider = generatorContext.CompilationProvider.Combine(enums.Collect());
        generatorContext.RegisterSourceOutput(provider, (context, tuple) => GenerateAllEnumClasses(tuple.Left, tuple.Right, context));
    }

    private static void GenerateAllEnumClasses(Compilation compilation,
                                               ImmutableArray<EnumDeclarationSyntax> enums,
                                               SourceProductionContext context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        var enumInfos = GetTypesToGenerate(compilation, enums, context.CancellationToken);
        foreach (var enumInfo in enumInfos)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("namespace Default;");
            builder.Append("public abstract partial class ");
            builder.Append(enumInfo.Name);
            builder.AppendLine("{");
            foreach (var enumValue in enumInfo.EnumNames)
            {
                var enumClassName = $"{enumValue}Value";

                builder.AppendLine($"public static readonly {enumClassName} {enumValue} = new()");

                builder.AppendLine($"public partial class {enumClassName}: {enumInfo.Name}");
                builder.AppendLine("{ }");
            }
            context.AddSource($"{enumInfo.Name}EnumClass.g.cs", builder.ToString());
        }
    }

    private static List<EnumInfo> GetTypesToGenerate(Compilation compilation,
                                                   ImmutableArray<EnumDeclarationSyntax> enums,
                                                   CancellationToken ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeFullName);
        var enumInfos = new List<EnumInfo>();
        if (enumClassAttributeSymbol is null)
        {
            return enumInfos;
        }

        foreach (var syntax in enums)
        {
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(syntax) is not INamedTypeSymbol enumSymbol)
            {
                continue;
            }
            var memberNames = enumSymbol.GetMembers()
                                        .Where(member => member is IFieldSymbol {ConstantValue: not null})
                                        .Select(x => x.Name)
                                        .ToList();
            enumInfos.Add(new EnumInfo(enumSymbol.Name, memberNames));
        }

        return enumInfos;
    }
    
    private static bool FilterEnumDeclarations(SyntaxNode node, CancellationToken token)
    {
        return node is EnumDeclarationSyntax {AttributeLists.Count: > 0};
    }

    private static EnumDeclarationSyntax? GetSemanticModelForEnumClass(GeneratorSyntaxContext context, CancellationToken token)
    {
        var syntax = ( EnumDeclarationSyntax ) context.Node;
        var attributeLists = syntax.AttributeLists;
        for (var i = 0; i < attributeLists.Count; i++)
        {
            var attributes = attributeLists[i].Attributes;
            for (var j = 0; j < attributes.Count; j++)
            {
                var attributeSyntax = attributes[j];
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var containingTypeSymbol = attributeSymbol.ContainingType?.ToDisplayString();
                if (containingTypeSymbol is not Constants.EnumClassAttributeFullName)
                {
                    continue;
                }

                return syntax;
            }
        }

        return null;
    }
}