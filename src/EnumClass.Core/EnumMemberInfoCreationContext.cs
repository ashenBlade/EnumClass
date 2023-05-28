using EnumClass.Core.SymbolName;

namespace EnumClass.Core;

/// <summary>
/// Helpful information for constructing enum member class types 
/// </summary>
/// <param name="EnumClassName">
/// Name for generating enum class
/// </param>
/// <param name="Namespace">
/// Namespace where enum class will be generated
/// </param>
internal readonly record struct EnumMemberInfoCreationContext(ISymbolName EnumClassName, ISymbolName Namespace, ISymbolName EnumName);