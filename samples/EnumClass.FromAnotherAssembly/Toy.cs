using EnumClass.Attributes;
using Toy = EnumClass.EnumOnly.Toy;

[assembly: ExternalEnumClass(typeof(Toy), Namespace = "EnumClass.FromAnotherAssembly")]
namespace EnumClass.FromAnotherAssembly;

public partial class Toy
{
    public abstract void Play();
    public partial class BallEnumValue
    {
        public override void Play()
        {
            Console.WriteLine($"Ball jumps high");
        }
    }

    public partial class CarEnumValue
    {
        public override void Play()
        {
            Console.WriteLine($"Car is going fast");
        }
    }

    public partial class DollEnumValue
    {
        public override void Play()
        {
            Console.WriteLine($"Doll is dressed smartly");
        }
    }
}