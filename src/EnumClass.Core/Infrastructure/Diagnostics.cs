using Microsoft.CodeAnalysis;

namespace EnumClass.Core.Infrastructure;

public static class Diagnostics
{
    public static readonly DiagnosticDescriptor NoEnumClassAttributeFound = 
        new("ECG001",
            "No EnumClassAttribute found",
            "Could not find EnumClassAttribute. Add reference to package with EnumClass.Generator or add nuget package directly.", 
            "Usage",
            DiagnosticSeverity.Warning,
            true);
    
    public static readonly DiagnosticDescriptor NoEnumMemberInfoAttributeFound = 
        new("ECG002",
            "No EnumMemberInfoAttribute found",
            "Could not find EnumMemberInfoAttribute. Add reference to package with EnumClass.Generator or add nuget package directly.", 
            "Usage",
            DiagnosticSeverity.Warning,
            true);
}