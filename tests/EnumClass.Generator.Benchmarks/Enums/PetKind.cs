using EnumClass.Attributes;

namespace EnumClass.Generator.Benchmarks.Enums;

[EnumClass(Namespace = "EnumClassPetKind")]
public enum PetKind
{
    [StringValue("Kitten")]
    Cat,
    Dog,
    Hamster,
    Fish
}