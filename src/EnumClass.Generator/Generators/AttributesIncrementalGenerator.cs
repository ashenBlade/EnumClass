using Microsoft.CodeAnalysis;

namespace EnumClass.Generator.Generators;

[Generator]
public class AttributesIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            // Attribute made internal to not conflict with another existing attributes
            var sourceCode = @"using System;

namespace EnumClass.Generated
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    internal class EnumClassAttribute: Attribute
    { }
}";
            ctx.AddSource("EnumClassAttribute.g.cs", sourceCode);
        });
    }
}