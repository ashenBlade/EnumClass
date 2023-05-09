namespace EnumClass.Core.UnderlyingType;

/// <summary>
/// List of predefined types that can be used as underlying of enum
/// </summary>
public static class UnderlyingTypes
{
    public static readonly IUnderlyingType Int = new GeneralUnderlyingType("int", s => int.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Uint = new GeneralUnderlyingType("uint", s => uint.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Long = new GeneralUnderlyingType("long", s => long.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Ulong = new GeneralUnderlyingType("ulong", s => ulong.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Byte = new GeneralUnderlyingType("byte", s => byte.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Sbyte = new GeneralUnderlyingType("sbyte", s => sbyte.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Short = new GeneralUnderlyingType("short", s => short.Parse(s).GetHashCode());
    public static readonly IUnderlyingType Ushort = new GeneralUnderlyingType("ushort", s => ushort.Parse(s).GetHashCode());
}