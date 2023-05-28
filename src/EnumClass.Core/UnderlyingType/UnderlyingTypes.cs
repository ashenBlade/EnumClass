namespace EnumClass.Core.UnderlyingType;

/// <summary>
/// List of predefined types that can be used as underlying of enum
/// </summary>
public static class UnderlyingTypes
{
    public static readonly IUnderlyingType Int = new GeneralUnderlyingType("int", "Int32",  s => int.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Uint = new GeneralUnderlyingType("uint", "UInt32", s => uint.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Long = new GeneralUnderlyingType("long", "Int64", s => long.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Ulong = new GeneralUnderlyingType("ulong", "UInt64", s => ulong.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Byte = new GeneralUnderlyingType("byte", "Byte", s => byte.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Sbyte = new GeneralUnderlyingType("sbyte", "SByte", s => sbyte.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Short = new GeneralUnderlyingType("short", "Int16", s => short.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Ushort = new GeneralUnderlyingType("ushort", "UInt16", s => ushort.Parse(s).GetHashCode());
}