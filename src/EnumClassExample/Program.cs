using System;
using EnumClassExample;

PrintEnumClassComparison("Dog", EnumClassExample.EnumClass.PetKind.Dog, PetKind.Dog);
PrintEnumClassComparison("Cat", EnumClassExample.EnumClass.PetKind.Cat, PetKind.Cat);
PrintEnumClassComparison("Parrot", EnumClassExample.EnumClass.PetKind.Parrot, PetKind.Parrot);
PrintEnumClassComparison("Hamser", EnumClassExample.EnumClass.PetKind.Hamster, PetKind.Hamster);

void PrintEnumClassComparison(string representationName, EnumClassExample.EnumClass.PetKind enumClassKind, PetKind enumKind)
{
    Console.WriteLine($"Comparison for {representationName}");
    Console.WriteLine($"Enum class:");
    Console.WriteLine($"    ToString: {enumClassKind.ToString()}");
    Console.WriteLine($"    Integer value: {enumClassKind.Value}");
    Console.WriteLine($"Raw enum:");
    Console.WriteLine($"    ToString: {enumKind.ToString()}");
    Console.WriteLine($"    Integer value: {(int)enumKind}");

    Console.WriteLine($"Equals: {enumClassKind.Equals(enumKind)}");
    Console.WriteLine($"==: {enumClassKind == enumKind}");
    Console.WriteLine($"!=: {enumClassKind == enumKind}");
}