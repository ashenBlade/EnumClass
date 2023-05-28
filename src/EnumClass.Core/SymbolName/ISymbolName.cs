namespace EnumClass.Core.SymbolName;

/// <summary>
/// Interface for convenient work with symbol names
/// </summary>
public interface ISymbolName
{
    /// <summary>
    /// Fully qualified element name.
    /// Usually with 'global::' prefix
    /// </summary>
    /// <example>
    /// global::EnumClass.Models.PetKind - fully qualified enum name
    /// </example>
    public string FullyQualified { get; }
    
    /// <summary>
    /// Plain name of element.
    /// </summary>
    /// <example>
    /// PetKind - only enum name
    /// </example>
    public string Plain { get; }
}