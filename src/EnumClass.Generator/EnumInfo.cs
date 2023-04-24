using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator;

internal class EnumInfo
{
    public string ClassName { get; }
    public string FullyQualifiedEnumName { get; }
    public string Namespace { get; }
    public EnumValueInfo[] Values { get; init; }
    
    public EnumInfo(string enumName, string @namespace, EnumValueInfo[] values)
    {
        FullyQualifiedEnumName = SymbolDisplay.FormatLiteral( $"{@namespace}.{enumName}", false );
        Namespace = SymbolDisplay.FormatLiteral( @namespace, false );
        Values = values;
        ClassName = SymbolDisplay.FormatLiteral(enumName, false);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(( EnumInfo ) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = ClassName.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ FullyQualifiedEnumName.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ Namespace.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ Values.GetHashCode();
            return hashCode;
        }
    }

    public bool Equals(EnumInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ClassName == other.ClassName
            && FullyQualifiedEnumName == other.FullyQualifiedEnumName
            && Namespace == other.Namespace
            && Values.Equals(other.Values);
    }
}