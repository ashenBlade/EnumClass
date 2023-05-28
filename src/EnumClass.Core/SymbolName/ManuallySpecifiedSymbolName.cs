namespace EnumClass.Core.SymbolName;

public class ManuallySpecifiedSymbolName: ISymbolName
{
    public ManuallySpecifiedSymbolName(string fullyQualified, string plain)
    {
        FullyQualified = fullyQualified;
        Plain = plain;
    }

    public string FullyQualified { get; }
    public string Plain { get; }
}