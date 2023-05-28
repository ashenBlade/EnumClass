using EnumClass.Attributes;

namespace EnumClass.NestedEnum;

public partial class Staff
{
    [EnumClass(
        ClassName = nameof(Staff),
        Namespace = "EnumClass.NestedEnum")]
    public enum StaffType : long
    {
        Manager,
        Programmer,
        Tester,
        CTO,
        CEO,
    }
    public abstract void Speak();

    public partial class ProgrammerEnumValue
    {
        public override void Speak()
        {
            Console.WriteLine("0110011011");
        }
    }

    public partial class ManagerEnumValue
    {
        public override void Speak()
        {
            Console.WriteLine("Deadline was yesterday!");
        }
    }

    public partial class CTOEnumValue
    {
        public override void Speak()
        {
            Console.WriteLine("We need microservices!");
        }
    }

    public partial class CEOEnumValue
    {
        public override void Speak()
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("EnumClass is the best product!!!");
            Console.ForegroundColor = old;
        }
    }

    public partial class TesterEnumValue
    {
        public override void Speak()
        {
            Console.WriteLine(string.Join(',', Enumerable.Range(0, 100).Select(_ => Random.Shared.Next().ToString())));
        }
    }
}