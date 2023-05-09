using System;

namespace EnumClass.Core;

public class GeneralUnderlyingType: IUnderlyingType
{
    private readonly Func<string, int> _hashCodeComputer;

    public GeneralUnderlyingType(string name, Func<string, int> hashCodeComputer)
    {
        _hashCodeComputer = hashCodeComputer;
        Name = name;
    }

    public string Name { get; }
    public int ComputeHashCode(string integralValue) => _hashCodeComputer(integralValue);
}