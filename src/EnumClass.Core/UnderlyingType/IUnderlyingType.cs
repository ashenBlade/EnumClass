namespace EnumClass.Core.UnderlyingType;

/// <summary>
/// Interface that represents underlying type of enum
/// </summary>
/// <example>int, byte, long, ulong</example>
public interface IUnderlyingType
{
    /// <summary>
    /// Name of the underlying base integral type as C# keyword as plain string
    /// </summary>
    /// <example>int, ushort, byte, long</example>
    public string CSharpKeyword { get; }
    
    /// <summary>
    /// Name of type as defined in CLR without "System" prefix
    /// </summary>
    /// <example>
    /// Byte, Int32, Int64, UInt64
    /// </example>
    public string ClrTypeName { get; }
    /// <summary>
    /// Try to compute hashcode for provider integral value.
    /// </summary>
    /// <exception cref="System.OverflowException">passed string represents a number less than MinValue or greater than MaxValue of given type</exception>
    /// <exception cref="System.FormatException">passed string is not of the correct format for given type</exception>
    /// <returns>Computed hash code</returns>
    public int ComputeHashCode(string integralValue);
}