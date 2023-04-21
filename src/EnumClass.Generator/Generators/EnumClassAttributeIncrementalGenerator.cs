using Microsoft.CodeAnalysis;

namespace EnumClass.Generator.Generators;

[Generator]
public class EnumClassAttributeIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            var sourceCode = @"using System;

namespace EnumClass
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    internal class EnumClassAttribute: Attribute
    { }
}";
            ctx.AddSource("EnumClassAttribute.g.cs", sourceCode);
        });
    }
}