using System.Text;
using EnumClass.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace EnumClass.JsonConverter.Generator;

[Generator]
public class JsonConverterIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext initializationContext)
    {
        initializationContext.RegisterSourceOutput(initializationContext.CompilationProvider, (context, compilation) =>
        {
            var enumsToGenerate = EnumInfoFactory.GetAllEnumInfosFromCompilation(compilation, context);
            if (enumsToGenerate is null)
            {
                return;
            }

            foreach (var enumInfo in enumsToGenerate)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                GenerateEnumJsonConverter(enumInfo, context);
            }
        });
    }


    private static void GenerateEnumJsonConverter(EnumInfo enumInfo, SourceProductionContext context)
    {
        var builder = new StringBuilder();
        builder.AppendLine(@"using System.Text.Json;
using System.Text.Json.Serialization;
using System;");

        builder.AppendFormat("namespace {0}\n{{\n", enumInfo.Namespace);
        
        // Class definition
        builder.AppendFormat("public class {0}JsonConverter : JsonConverter<{1}>\n{{\n", enumInfo.ClassName, enumInfo.FullyQualifiedClassName);
        
        // Implement "Read" method for deserialization
        builder.AppendFormat(
            "    public override {0} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)\n", enumInfo.FullyQualifiedClassName);
        builder.AppendLine("    {");
        
        // We are deserializing using integral representation of enum
        // For this purpose, Utf8JsonSerializer has TryGet* methods in which second parts are clr name of integral type
        // ClrTypeName was added to IUnderlyingType primarily for such cases
        builder.AppendFormat("        if (reader.TryGet{0}(out var value) && {1}.TryParse(value, out var result))\n", enumInfo.UnderlyingType.ClrTypeName, enumInfo.FullyQualifiedClassName);
        builder.AppendLine("        {");
        builder.AppendLine("            return result;");
        builder.AppendLine("        }");
        builder.AppendLine("        return null;");
        builder.AppendLine("    }");
        builder.AppendLine();
        
        // Implement "Write" method for serialization
        builder.AppendFormat(
            "    public override void Write(Utf8JsonWriter writer, {0} value, JsonSerializerOptions options)\n", enumInfo.FullyQualifiedClassName);
        builder.AppendLine("    {");
        builder.AppendFormat("        writer.WriteNumberValue(({0}) value);\n", enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendLine("    }");
        builder.AppendLine();
        
        builder.AppendLine("}");
        builder.AppendLine("}");
        
        context.AddSource($"{enumInfo.ClassName}JsonConverter.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }
}