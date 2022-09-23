using System;
using System.ComponentModel;

namespace Proxfield.Text.Ini.Extensions
{
    public static class TypeExtension
    {
        public static object? ConvertTo(this object? obj, Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                NullableConverter nullableConverter = new NullableConverter(type);
                type = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(obj, type);
        }
    }
}