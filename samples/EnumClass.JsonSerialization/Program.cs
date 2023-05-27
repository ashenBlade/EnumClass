using System.Text.Json;
using EnumClass.JsonSerialization;
using EnumClass.JsonSerialization.EnumClass;
using CarType = EnumClass.JsonSerialization.EnumClass.CarType;

Console.WriteLine($"In this example we serialize CarType into into CarInfo object.");
Console.WriteLine($"This type contains single property \"Type\" of type \"CarType\".");
do
{
    Console.Write("Enter type of car: ");
    var inputType = Console.ReadLine();
    if (CarType.TryParse(inputType!, out var carType))
    {
        ShowSerializationForCarType(carType!);
        break;
    }

    Console.WriteLine($"Could not parse car type. Available types are:");
    foreach (var type in CarType.GetAllMembers())
    {
        Console.WriteLine($" - {type}");
    }
} while(true);


void ShowSerializationForCarType(CarType carType)
{
    var info = new CarInfo(carType);

    var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
    {
        Converters = {new CarTypeJsonConverter()}
    };
    var serialized = JsonSerializer.Serialize(info, serializerOptions);
    Console.WriteLine($"Serialized: {serialized}");
    var deserialized = JsonSerializer.Deserialize<CarInfo>(serialized, serializerOptions);
    Console.WriteLine($"Deserialized: {deserialized}");
}