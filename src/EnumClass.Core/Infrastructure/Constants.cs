namespace EnumClass.Core.Infrastructure;

public static class Constants
{
    public static class EnumClassAttributeInfo
    {
        public const string AttributeFullName = "EnumClass.Attributes.EnumClassAttribute";

        public static class NamedArguments
        {
            public const string Namespace = "Namespace";
            public const string ClassName = "ClassName";
        }
    }
    
    public static class EnumMemberInfoAttributeInfo
    {
        public const string AttributeFullName = "EnumClass.Attributes.EnumMemberInfoAttribute";

        public static class NamedArguments
        {
            public const string StringValue = "StringValue";
        }
    }
}