namespace EnumClass.Core.Infrastructure;

public static class Constants
{
    public static class EnumClassAttributeInfo
    {
        public const string AttributeFullName = "EnumClass.Attributes.EnumClassAttribute";
        public const string AttributeClassName = "EnumClass";
        
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

    // ReSharper disable once MemberHidesStaticFromOuterClass
    public static class ExternalEnumClassAttributeInfo
    {
        public const string AttributeFullName = "EnumClass.Attributes.ExternalEnumClassAttribute";
        public const string AttributeClassName = "ExternalEnumClass";
        public static class NamedArguments
        {
            public const string Namespace = "Namespace";
            public const string ClassName = "ClassName";
        }
    }
}