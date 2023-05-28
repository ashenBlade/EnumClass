# EnumClass.JsonConverter.Generator

## Summary

Source generator that generates custom `JsonConverter<T>` (in System.Text.Json) for generated enum classes

## Installation

1. Add `EnumClass.Generator` NuGet package to first project (with enum)
2. Mark required enum with `[EnumClass]` in first project
3. Reference first project from second
4. Add `EnumClass.JsonConverter.Generator` NuGet package to second project
5. Build second project

That's it! JsonConverter will be in same namespace as generated enum class

> P.S. It also works when `EnumClass.JsonConverter.Generator` added to first project 

## Examples

Assume that we have single project with `EnumClass.Generator` and `EnumClass.JsonConverter.Generator` packages added.

```csharp
// TestEnum.cs
namespace Test;

[EnumClass]
public enum TestEnum
{
    First = 1,
    Second = 2
}
```

```csharp
// Dto.cs
using Test.EnumClass;

namespace Test;

public class Dto
{
    public TestEnum Value { get; set; }
}
```

```csharp
// Program.cs
using System;
using Test.EnumClass;
using System.Text.Json;

var json = @"
{
    ""Value"": 2
}";

var deserialized = JsonSerializer.Deserialize<Dto>(json, new JsonSerializerOptions(JsonSerializerDefaults.General)
    {
        Converters = 
        {
            // Was generated
            new TestEnumJsonConverter()
        }
    });

Console.WriteLine(deserialized.Value);

// Output: Second
```

For real project example refer to [samples/EnumClass.JsonSerialization](../../samples/EnumClass.JsonSerialization)

## Remarks

If no EnumClass or EnumMemberInfo attributes found you will see warning after build.
This usually means that you don't have EnumClass added to child projects.