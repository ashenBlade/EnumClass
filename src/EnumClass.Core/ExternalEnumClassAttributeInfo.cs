using Microsoft.CodeAnalysis;

namespace EnumClass.Core;

public record struct ExternalEnumClassAttributeInfo(INamedTypeSymbol EnumSymbol)
{
    public string? ClassName { get; set; } = null;
    public string? Namespace { get; set; } = null;
}