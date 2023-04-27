using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator;

public class EnumValueInfo
{
    private readonly string _stringRepresentation;
    public string IntValue { get; }
    public string Name { get; }
    public string ClassName { get; }
    public string FullyQualifiedName { get; }
    public EnumValueInfo(string name, string enumName, int value)
    :this(name, enumName, value, name)
    {
        // IntValue = SymbolDisplay.FormatPrimitive(value, false, false);
        // Name = SymbolDisplay.FormatLiteral(name, false);
        // ClassName = SymbolDisplay.FormatLiteral( $"{name}EnumValue", false );
        // FullyQualifiedName = $"{@namespace}.{enumName}.{name}";
    }
    
    public EnumValueInfo(string name, string enumName, int value, string stringRepresentation)
    {
        IntValue = SymbolDisplay.FormatPrimitive(value, false, false);
        Name = SymbolDisplay.FormatLiteral(name, false);
        ClassName = SymbolDisplay.FormatLiteral( $"{name}EnumValue", false );
        FullyQualifiedName = $"{enumName}.{name}";
        
        _stringRepresentation = stringRepresentation;
    }

    public string GetStringRepresentationQuoted()
    {
        return SymbolDisplay.FormatLiteral(_stringRepresentation, true);
    }
}