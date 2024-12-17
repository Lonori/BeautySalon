using System;

namespace BeautySalon
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateWithoutTimeAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextMultilineAttribute : Attribute
    { }
}
