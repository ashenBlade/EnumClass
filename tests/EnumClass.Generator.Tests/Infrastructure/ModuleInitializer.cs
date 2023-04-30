using System.Runtime.CompilerServices;

namespace EnumClass.Generator.Tests.Infrastructure;


public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}