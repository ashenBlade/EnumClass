using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using EnumClass.Core;
using EnumClass.Core.Infrastructure;
using EnumClass.Core.Models;
using EnumClass.Generator.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator;

[Generator(LanguageNames.CSharp)]
public class EnumClassIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        var directEnums = generatorContext
                         .SyntaxProvider
                         .CreateSyntaxProvider(
                              predicate: EnumDeclarationSyntaxPredicate, 
                              transform: EnumDeclarationSyntaxTransform)
                         .Where(x => x is not null);

        var provider = generatorContext.CompilationProvider.Combine(directEnums.Collect());
        generatorContext.RegisterSourceOutput(provider, (context, tuple) => GenerateEnumClasses(tuple.Left, tuple.Right!, context));

        var externalEnums = generatorContext
                           .SyntaxProvider
                           .CreateSyntaxProvider(
                                predicate: ExternalEnumClassAttributePredicate,
                                transform: ExternalEnumClassAttributeTransform)
                           .Where(x => x is not null);
        
        generatorContext.RegisterSourceOutput(generatorContext.CompilationProvider.Combine(externalEnums.Collect()), 
            (context, tuple) => GenerateExternalEnumClass(tuple.Left, tuple.Right, context));
    }

    private static bool EnumDeclarationSyntaxPredicate(SyntaxNode node, CancellationToken _)
    {
        return node is EnumDeclarationSyntax
                       {
                           AttributeLists: {Count: > 0} attributes
                       } && 
               attributes.Any(attr => attr.Name.Contains(Constants.EnumClassAttributeInfo.AttributeClassName));
    }

    private static AttributeSyntax ExternalEnumClassAttributeTransform(GeneratorSyntaxContext context, CancellationToken _)
    {
        var attributes = ( ( AttributeListSyntax ) context.Node ).Attributes;
        return attributes.FirstOrDefault(attr => 
            attr.Name
                .ToFullString()
                .Contains(Constants.ExternalEnumClassAttributeInfo.AttributeClassName))!;
    }

    private static bool ExternalEnumClassAttributePredicate(SyntaxNode node, CancellationToken _)
    {
        return node is AttributeListSyntax {Attributes.Count: > 0} attributeListSyntax && 
               // [assembly: ...
               ( attributeListSyntax.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) ?? false ) &&
               attributeListSyntax.Attributes.Any(
                   // [...ExternalEnumClass(
                   attribute => attribute.Name.Contains(Constants.ExternalEnumClassAttributeInfo.AttributeClassName) &&
                                // typeof(T), ...]
                                attribute is
                                {
                                    ArgumentList.Arguments: { Count: >0 } arguments
                                } && arguments[0].Expression.IsKind(SyntaxKind.TypeOfExpression));
    }

    private void GenerateExternalEnumClass(Compilation compilation,
                                           ImmutableArray<AttributeSyntax> attributes,
                                           SourceProductionContext context)
    {
        if (attributes.IsDefaultOrEmpty)
        {
            return;
        }
        
        var infos = new List<ExternalEnumClassAttributeInfo>(attributes.Length);

        // Collect all types that were marked to be generated
        foreach (var attribute in attributes)
        {
            // Get enum type we want to construct
            if (attribute is not
                {
                    ArgumentList.Arguments:
                    {
                        Count: >0
                    } arguments
                } ||
                // First must be typeof(T) expression
                arguments[0].Expression is not TypeOfExpressionSyntax typeOfExpression) 
            {
                continue;
            }

            // Extract enum type
            if (compilation.GetSemanticModel(attribute.SyntaxTree)
                           .GetSymbolInfo(typeOfExpression.Type)
                           .Symbol is not INamedTypeSymbol
                                          {
                                              // This is not null only for enums
                                              EnumUnderlyingType: not null,
                                          } enumType)
            {
                continue;
            }

            var info = new ExternalEnumClassAttributeInfo(enumType);

            for (var i = 1; i < attribute.ArgumentList.Arguments.Count; i++)
            {
                var argument = attribute.ArgumentList.Arguments[i];
                if (argument.Expression is not LiteralExpressionSyntax
                                               {
                                                   Token:
                                                   {
                                                       Value: not null,
                                                       ValueText: var value,
                                                   } token,
                                               }
                    // Only accept single line string literals
                 || !token.IsKind(SyntaxKind.StringLiteralToken))
                {
                    continue;
                }

                switch (argument.NameEquals?.Name.Identifier.Text)
                {
                    case Constants.ExternalEnumClassAttributeInfo.NamedArguments.Namespace:
                        info = info with {Namespace = value};
                        break;
                    case Constants.ExternalEnumClassAttributeInfo.NamedArguments.ClassName:
                        info = info with {ClassName = value};
                        break;
                }
            }
            
            infos.Add(info);
        }
        
        var generationContext =
            new GenerationContext(compilation.Options.NullableContextOptions is not NullableContextOptions.Disable);
        
        foreach (var info in infos)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            var enumInfo = EnumInfoFactory.CreateFromExternalEnumNamedTypeSymbol(info);
            GeneratorHelpers.GenerateEnumClass(enumInfo, context, generationContext);
        }
    }
    
    private static void GenerateEnumClasses(Compilation                           compilation,
                                            ImmutableArray<EnumDeclarationSyntax> enums,
                                            SourceProductionContext               context)
    {
        // Do not use EnumInfoFactory that accepts compilation, 
        // because it will search for all enums in all assemblies
        // but we need only enums from this assembly
        
        // Why do i need to compile? Skip!
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        // Extract all EnumInfo from found syntax list
        var enumInfos = GetAllEnumsToGenerate(compilation, enums, context.CancellationToken);
        
        // Return if EnumInfos not found
        if (enumInfos is null or {Count: 0})
        {
            return;
        }
        
        var generationContext = new GenerationContext(
            nullableEnabled: compilation.Options.NullableContextOptions is not NullableContextOptions.Disable);
        
        foreach (var enumInfo in enumInfos)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            GeneratorHelpers.GenerateEnumClass(enumInfo, context, generationContext);
        }
    }

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    private static EnumDeclarationSyntax? EnumDeclarationSyntaxTransform(GeneratorSyntaxContext context, CancellationToken token)
    {
        
        var syntax         = ( EnumDeclarationSyntax ) context.Node;
        var attributeLists = syntax.AttributeLists;
        
        // Flatten all attribute lists
        for (var i = 0; i < attributeLists.Count; i++)
        {
            var attributes = attributeLists[i].Attributes;
            for (var j = 0; j < attributes.Count; j++)
            {
                var attributeSyntax = attributes[j];

                // Constructor is the method
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }
                
                // Check attribute name with plain text,
                // because now we do not have access to ISymbol of Attribute
                var containingTypeSymbol = attributeSymbol.ContainingType?
                                                          .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                                                          .Replace("global::", "");
                if (containingTypeSymbol is not Constants.EnumClassAttributeInfo.AttributeFullName)
                {
                    continue;
                }

                return syntax;
            }
        }

        return null;
    }
    
    /// <summary>
    /// Create EnumInfo domain object representing enum class with dependencies (names,  members etc)
    /// </summary>
    /// <param name="compilation">Compilation context, where <paramref name="enums"/> were found</param>
    /// <param name="enums">All <c>enum</c> declarations. They will be filtered to be annotated with [EnumClass]</param>
    /// <param name="ct">Cancellation token from user compilation</param>
    /// <returns>List of <see cref="EnumInfo"/> with length > 0 if successfully parsed, or null otherwise</returns>
    private static List<EnumInfo>? GetAllEnumsToGenerate(Compilation                           compilation,
                                                         ImmutableArray<EnumDeclarationSyntax> enums,
                                                         CancellationToken                     ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeInfo.AttributeFullName);
        if (enumClassAttributeSymbol is null)
        {
            return null;
        }
        
        var enumInfos = new List<EnumInfo>(enums.Length);

        var enumMemberInfoAttribute = compilation.GetTypeByMetadataName(Constants.EnumMemberInfoAttributeInfo.AttributeFullName);
        
        foreach (var syntax in enums)
        {
            // Do check twice if we get cancel request in between creating EnumInfo
            // Single check might fail if 'enums' contains single element and
            // cancellation happened while creating EnumInfo
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            
            // Sanity check
            if (semanticModel.GetDeclaredSymbol(syntax) is not { EnumUnderlyingType: not null } enumSymbol)
            {
                continue;
            }
            
            var enumInfo = EnumInfoFactory.CreateFromNamedTypeSymbol(enumSymbol, enumClassAttributeSymbol, enumMemberInfoAttribute!);
            ct.ThrowIfCancellationRequested();
            enumInfos.Add(enumInfo);
        }

        return enumInfos.Count > 0 
                   ? enumInfos 
                   : null;
    }
}