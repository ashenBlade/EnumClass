using System.ComponentModel.DataAnnotations;
using EnumClass.Generated;

namespace EnumClassExample;

[EnumClass]
public enum PetKind
{
    [Display(Name = "Sample dog")]
    Dog,
    Cat,
    Parrot,
    Hamster,
}