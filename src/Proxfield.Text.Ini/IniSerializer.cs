using Proxfield.Text.Ini.Constants;
using Proxfield.Text.Ini.Extensions;
using System.Reflection;
using System.Text;

namespace Proxfield.Text.Ini
{
    /// <summary>
    /// IniSerializer
    /// </summary>
    public class IniSerializer
    {
        /// <summary>
        /// Serializes an object to Ini Format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T? obj, IniSerializerSettings? settings = null)
        {
            if (obj == null) return string.Empty;
            settings ??= new IniSerializerSettings();
            return SerializeObjectInternal(obj!, typeof(T), settings);
        }
        /// <summary>
        /// Serializes an object to Ini Format
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj, Type type, IniSerializerSettings? settings = null)
        {
            settings ??= new IniSerializerSettings();
            return SerializeObjectInternal(obj, type, settings);
        }
        /// <summary>
        /// Get IniFile from string content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IniFile GetIniFile(string content)
        {
            var sections = new List<IniSection>();
            IniSection? section = null;
            content.ToLines().ForEach(e =>
            {
                if (e.IsSection())
                {
                    section = new IniSection(e.GetSectionName());
                    sections.Add(section);
                }

                if (e.IsKeyPar())
                    section?.Add(new IniValue(e.GetAttributeNameValue()));
            });

            return new IniFile(sections);
        }
        /// <summary>
        /// Deserialize content to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T? DeserializeObject<T>(string content)
        {
            var instance = (T?)Activator.CreateInstance(typeof(T));
            PropertyInfo[] objectProps = typeof(T).GetProperties();
            object? innerProperty = instance;

            var objects = new List<KeyValuePair<string, object?>>();
            var section = typeof(T).Name;

            content.ToLines().ForEach(e =>
            {
                if (e.IsSection() && !e.GetSectionName().Equals(typeof(T).Name))
                {
                    section = e.GetSectionName();
                    var property = objectProps.Where(p => p.Name.Equals(section)).First();

                    innerProperty = Activator.CreateInstance(property!.PropertyType);
                    objectProps = innerProperty!.GetType().GetProperties();
                }

                if (e.IsKeyPar())
                {
                    var propertyKeyPar = e.GetAttributeNameValue();
                    var property = innerProperty!
                        .GetType()
                        .GetProperties()
                        .Where(c => c.Name.Equals(propertyKeyPar.Key))
                        .FirstOrDefault();

                    property?.SetValue(innerProperty, propertyKeyPar.Value?.ConvertTo(property.PropertyType));
                }

                if (objects.Where(p => p.Key.Equals(section)).Any())
                {
                    var remove = objects.Where(p => p.Key.Equals(section)).First();
                    var index = objects.IndexOf(remove);
                    objects.RemoveAt(index);
                }

                objects.Add(new KeyValuePair<string, object?>(section, innerProperty));
            });

            var mainOne = (T?)objects.FirstOrDefault(p => p.Key.Equals(typeof(T).Name)).Value;
            return FullFill<T>(mainOne, objects);
        }

        private static T? FullFill<T>(T? obj, List<KeyValuePair<string, object?>> objects)
        {
            obj?.GetType()
                  .GetProperties()
                .Where(p => p.IsNotPrimitive())
                .ToList()
                .ForEach(c =>
                {
                    var item = objects.Where(e => e.Key.Equals(c.Name)).FirstOrDefault();
                    if (item.Value != null)
                    {
                        var properties = item.Value?.GetType().GetProperties().Where(item => item.IsNotPrimitive());

                        if (properties?.Any() ?? false)
                        {
                            c.SetValue(obj, FullFill(item!.Value, objects));
                        }
                        else
                        {
                            c.SetValue(obj, item.Value);
                        }


                    }
                });

            return obj;
        }

        private static string Read(object obj, Type type, string path)
        {
            var builder = new StringBuilder();
            PropertyInfo[] props = type.GetProperties();
            props
                .Where(p => p.IsPrimitive())
                .ToList()
                .ForEach(p =>
                {
                    builder.AppendLine($"{p.Name}={p.GetValue(obj)}");
                });
            props
               .Where(p => p.IsNotPrimitive())
               .ToList()
               .ForEach(p =>
               {
                   builder.AppendLine($"[{path}.{p.Name}]");
                   builder.Append(Read(p.GetValue(obj), p.PropertyType, $"{path}.{p.Name}"));
               });
            return builder.ToString();
        }
        /// <summary>
        /// Internal method for serializing objects
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static string SerializeObjectInternal(object obj, Type type, IniSerializerSettings settings)
        {
            return $"[{type.Name}]\n{Read(obj, type, type.Name)}";
        }

    }
}