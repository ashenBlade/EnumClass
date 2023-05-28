using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace EnumClass.Core;

internal static class FactoryHelpers
{
    public static IEnumerable<INamedTypeSymbol> ExtractAllEnumsFromCompilation(Compilation compilation)
    {
        // https://stackoverflow.com/a/74163439/14109140
        
        // Cache of already processed assemblies and types
        var processedAssemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
        var foundEnumSymbols = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        
        // First, process main assembly - from which compilation was initiated
        foundEnumSymbols.UnionWith(ExtractEnumsFromAssembly(compilation.Assembly));
        processedAssemblies.Add(compilation.Assembly);
        
        // Then extract all other found enums from others
        foreach (var assemblySymbol in compilation.SourceModule.ReferencedAssemblySymbols)
        {
            if (processedAssemblies.Contains(assemblySymbol))
                continue;

            var foundEnums = ExtractEnumsFromAssembly(assemblySymbol);
            foundEnumSymbols.UnionWith(foundEnums);
            processedAssemblies.Add(assemblySymbol);
        }

        return foundEnumSymbols;
    }

    
    private static IEnumerable<INamedTypeSymbol> ExtractEnumsFromAssembly(IAssemblySymbol assemblySymbol)
    {
        foreach (var @namespace in GetAllNamespaces(assemblySymbol.GlobalNamespace))
        {
            foreach (var member in @namespace.GetTypeMembers())
            {
                foreach (var childOrSelf in GetAllNestedTypesAndSelf(member))
                {
                    if (IsRequiredEnum(childOrSelf))
                    {
                        yield return childOrSelf;
                    }            
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsRequiredEnum(INamedTypeSymbol typeSymbol)
    {
        // For now keep it that simple.
        // Maybe in the future we should add support for Accessibility (public, protected), but not now
        return typeSymbol.TypeKind is TypeKind.Enum;
    }

    private static IEnumerable<INamespaceSymbol> GetAllNamespaces(INamespaceSymbol root)
    {
        yield return root;
        foreach (var child in root.GetNamespaceMembers())
        {
            foreach (var next in GetAllNamespaces(child))
            {
                yield return next;
            }
        }
    }

    private static IEnumerable<INamedTypeSymbol> GetAllNestedTypesAndSelf(INamedTypeSymbol namedTypeSymbol)
    {
        yield return namedTypeSymbol;
        
        if (namedTypeSymbol.GetTypeMembers() is {Length:>0} namedTypeSymbols)
        {
            foreach (var member in namedTypeSymbols)
            {
                foreach (var type in GetAllNestedTypesAndSelf(member))
                {
                    yield return type;
                }
            }
        }
    }
}