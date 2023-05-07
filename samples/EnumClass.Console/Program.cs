using System;
using EnumClass.SimpleEnum.EnumClass;

PrintEnumClassComparison("Dog", PetKind.Cat, EnumClass.SimpleEnum.PetKind.Dog);
PrintEnumClassComparison("Cat", PetKind.Cat, EnumClass.SimpleEnum.PetKind.Cat);
PrintEnumClassComparison("Parrot", PetKind.Parrot, EnumClass.SimpleEnum.PetKind.Parrot);
PrintEnumClassComparison("Hamster", PetKind.Hamster, EnumClass.SimpleEnum.PetKind.Hamster);

void PrintEnumClassComparison(string representationName, PetKind enumClassKind, EnumClass.SimpleEnum.PetKind enumKind)
{
    Console.WriteLine($"Comparison for {representationName}");
    Console.WriteLine($"Enum class:");
    Console.WriteLine($"    ToString: {enumClassKind}");
    Console.WriteLine($"    Integer value: {(int)enumClassKind}");
    Console.WriteLine($"Raw enum:");
    Console.WriteLine($"    ToString: {enumKind}");
    Console.WriteLine($"    Integer value: {(int)enumKind}");

    Console.WriteLine($"Equals: {enumClassKind.Equals(enumKind)}");
    Console.WriteLine($"==: {enumClassKind == enumKind}");
    Console.WriteLine($"!=: {enumClassKind == enumKind}");
    
    var result = enumClassKind.Switch(2, 2,
        static (dog,     i, j) => i + j * 2,
        static (cat,     i, j) => cat.CalculateValue(i, j),
        static (parrot,  i, j) => i * j + 1,
        static (hamster, i, j) => i + j);
    Console.WriteLine($"It says that 2+2 is {result}");
    
    var sampleValue = enumClassKind.Switch(
        dog => 12,
        cat => 34,
        parrot => 56,
        hamster => 78);
    Console.WriteLine($"It's value is \"{sampleValue}\"");

    Console.WriteLine($"It's average weight is {enumClassKind.AverageWeight}");
    Console.WriteLine("-----------------------------------");
}