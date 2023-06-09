using System.Text;

namespace EnumClass.Generator;

public class GenerationContext
{
    private readonly StringBuilder _builder = new();

    public GenerationContext(bool nullableEnabled)
    {
        NullableEnabled = nullableEnabled;
    }

    public bool NullableEnabled { get; }
    
    public StringBuilder GetBuilder()
    {
        _builder.Clear();
        return _builder;
    }
}