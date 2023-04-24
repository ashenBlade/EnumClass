using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator;

public class EnumValueInfo
{
    public string IntValue { get; }
    public string Name { get; }
    public string ClassName { get; }
    public string FullyQualifiedName { get; }
    public EnumValueInfo(string name, string enumName, string @namespace, int value)
    {
        IntValue = SymbolDisplay.FormatPrimitive(value, false, false);
        Name = SymbolDisplay.FormatLiteral(name, false);
        ClassName = SymbolDisplay.FormatLiteral( $"{name}EnumValue", false );
        FullyQualifiedName = $"{@namespace}.{enumName}.{name}";
    }

    public string GetStringRepresentationQuoted()
    {
        return SymbolDisplay.FormatLiteral(Name, true);
    }
}