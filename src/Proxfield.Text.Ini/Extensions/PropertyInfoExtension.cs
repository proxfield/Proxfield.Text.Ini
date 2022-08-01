using System.Reflection;

namespace Proxfield.Text.Ini.Extensions
{
    public static class PropertyInfoExtension
    {
        public static string[] PrimitiveTypes = new[]
        {
            typeof(DateTime?).Name,
            typeof(string).Name,
            typeof(bool).Name,
            typeof(bool?).Name,
            typeof(int).Name,
            typeof(int?).Name,
            typeof(long).Name,
            typeof(long?).Name,
            typeof (short).Name,
            typeof (short?).Name,
            typeof(DateTime).Name,
        };

        public static bool IsPrimitive(this PropertyInfo property)
            => PrimitiveTypes.Contains(property.PropertyType.Name);

        public static bool IsNotPrimitive(this PropertyInfo property)
            => !PrimitiveTypes.Contains(property.PropertyType.Name);
    }
}