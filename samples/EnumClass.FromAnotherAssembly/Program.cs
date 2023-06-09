using System.Threading.Channels;
using EnumClass.FromAnotherAssembly;

foreach (var member in Toy.GetAllMembers())
{
    Console.WriteLine($"Playing with {member}:");
    member.Play();
    Console.WriteLine();
}