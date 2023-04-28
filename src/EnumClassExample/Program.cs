using System;
using EnumClassExample.EnumClass;

PrintEnumClassComparison("Dog", PetKind.Dog, EnumClassExample.PetKind.Dog);
PrintEnumClassComparison("Cat", PetKind.Cat, EnumClassExample.PetKind.Cat);
PrintEnumClassComparison("Parrot", PetKind.Parrot, EnumClassExample.PetKind.Parrot);
PrintEnumClassComparison("Hamster", PetKind.Hamster, EnumClassExample.PetKind.Hamster);

void PrintEnumClassComparison(string representationName, PetKind enumClassKind, EnumClassExample.PetKind enumKind)
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
    Console.Write("It wants to say: ");
    enumClassKind.Switch(
        dog =>
        {
            Console.WriteLine("I am the dog");    
        },
        cat =>
        {
            Console.WriteLine("I am the cat");
        }, 
        parrot =>
        {
            Console.WriteLine("I am the parrot");
        },
        hamster =>
        {
            Console.WriteLine("I am the hamster");
        });
    
    var sampleValue = enumClassKind.Switch(dog => 12, cat => 34, parrot => 56, hamster => 78);
    Console.WriteLine($"It's value is \"{sampleValue}\"");
    Console.WriteLine("-----------------------------------");
}
