//HintName: StringValueAttribute.g.cs
using System;

namespace EnumClass.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class StringValueAttribute: Attribute
    {
        internal StringValueAttribute(string value)
        { }
    }
}