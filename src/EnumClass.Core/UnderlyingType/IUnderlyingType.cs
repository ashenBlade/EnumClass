namespace EnumClass.Core;

public interface IUnderlyingType
{
    /// <summary>
    /// Name of the underlying base integral type
    /// </summary>
    /// <example>int, ushort, byte, long</example>
    public string Name { get; }
    /// <summary>
    /// Try to compute hashcode for provider integral value.
    /// </summary>
    /// <returns>Computed hash code</returns>
    public int ComputeHashCode(string integralValue);
}