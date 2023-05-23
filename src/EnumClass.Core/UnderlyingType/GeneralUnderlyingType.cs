using System;

namespace EnumClass.Core.UnderlyingType;

/// <summary>
/// Generic implementation for IUnderlying type
/// </summary>
public class GeneralUnderlyingType: IUnderlyingType
{
    private readonly Func<string, int> _hashCodeComputer;

    public GeneralUnderlyingType(string name, Func<string, int> hashCodeComputer)
    {
        _hashCodeComputer = hashCodeComputer;
        CSharpKeyword = name;
    }

    public string CSharpKeyword { get; }
    public int ComputeHashCode(string integralValue) => _hashCodeComputer(integralValue);
}