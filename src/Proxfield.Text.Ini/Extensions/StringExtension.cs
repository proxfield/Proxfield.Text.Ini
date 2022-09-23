using System.Collections.Generic;
using System.Linq;

namespace Proxfield.Text.Ini.Extensions
{
    public static class StringExtension
    {
        public static List<string> ToLines(this string text)
        {
            return text
                .Replace("\r", string.Empty)
                .Split("\n")
                .Where(line => !line.StartsWith(";"))
                .ToList();
        }
    }
}