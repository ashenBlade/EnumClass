using EnumClass.Attributes;

namespace EnumClass.Generator.Benchmarks.Enums;

[EnumClass(Namespace = "EnumClassPetKind")]
public enum PetKind
{
    [EnumMemberInfo(StringValue = "Kitten")]
    Cat,
    Dog,
    Hamster,
    Fish
}