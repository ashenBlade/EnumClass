// See https://aka.ms/new-console-template for more information

using EnumClass.NestedEnum;

Console.WriteLine("Enter staff name: ");

while (Console.ReadLine() is var name)
{
    if (Staff.TryParse(name, out var staff))
    {
        staff!.Speak();
        break;
    }
    
    Console.WriteLine("Could not recognize staff type. \nEnter again: ");
}