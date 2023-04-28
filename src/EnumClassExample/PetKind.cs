using System;
using System.ComponentModel.DataAnnotations;
using EnumClass.Generated;

namespace EnumClassExample
{
    [EnumClass]
    public enum PetKind
    {
        [Display(Name = "Sample dog")]
        Dog,
        Cat,
        Parrot = 50,
        Hamster,
    }
}

namespace EnumClassExample.EnumClass
{
    public partial class PetKind
    {
        /// <summary>
        /// Average weight of pet in kg
        /// </summary>
        public abstract double AverageWeight { get; }
        public partial class CatEnumValue: PetKind
        {
            public override double AverageWeight => 4.5;

            public int CalculateValue(int left, int right)
            {
                return Math.Clamp(left, left - 1000, left + right + 1000) * right;
            }
        }

        public partial class DogEnumValue
        {
            public override double AverageWeight => 20.5;
        }

        public partial class HamsterEnumValue
        {
            public override double AverageWeight => 0.05;
        }

        public partial class ParrotEnumValue
        {
            public override double AverageWeight => 0.055;
        }
    }
    
}