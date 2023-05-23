using System;

namespace EnumClass.Core.Accessibility;

public class GeneralAccessibility: IAccessibility
{
    public static readonly GeneralAccessibility Public = new("public");
    public static readonly GeneralAccessibility Internal = new("internal");
    public static readonly GeneralAccessibility Protected = new("protected");
    public static readonly GeneralAccessibility Private = new("private");
    public static readonly GeneralAccessibility ProtectedInternal = new("protected internal");

    public static GeneralAccessibility Default => Internal;
    
    public string Keyword { get; }
    
    private GeneralAccessibility(string keyword)
    {
        Keyword = keyword;
    }

    public static GeneralAccessibility FromAccessibility(Microsoft.CodeAnalysis.Accessibility accessibility) =>
        // Friend is like internal
        accessibility switch
        {
            Microsoft.CodeAnalysis.Accessibility.Private   => Private,
            
            Microsoft.CodeAnalysis.Accessibility.ProtectedAndInternal
                or Microsoft.CodeAnalysis.Accessibility.ProtectedAndFriend => ProtectedInternal,
            
            Microsoft.CodeAnalysis.Accessibility.Protected => Protected,
            
            Microsoft.CodeAnalysis.Accessibility.Internal 
             or Microsoft.CodeAnalysis.Accessibility.Friend => Internal,
            
            Microsoft.CodeAnalysis.Accessibility.ProtectedOrInternal
                or Microsoft.CodeAnalysis.Accessibility.ProtectedOrFriend => Protected,
            
            Microsoft.CodeAnalysis.Accessibility.Public   => Public,
            
            _ => Default,
        };
}