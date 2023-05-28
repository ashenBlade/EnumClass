using EnumClass.NestedEnum;

Console.WriteLine("Enter staff name: ");

while (Console.ReadLine() is {} name)
{
    if (Staff.TryParse(name, out var staff))
    {
        staff!.Speak();
        break;
    }
    
    Console.WriteLine("Could not recognize staff type. \nEnter again: ");
}