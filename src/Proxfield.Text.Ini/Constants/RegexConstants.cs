using System.Text.RegularExpressions;

namespace Proxfield.Text.Ini.Constants
{
    public static class RegexConstants
    {
        public static string SectionRegex => @"(\[)(([a-zA-Z]{0,})(\.{0,})){0,}(\])";
        public static string SubSectionRegex => @"";
        public static string KeyParRegex => @"([a-zA-Z]{0,})(=)(.{0,})";

        public static bool IsSection(this string line)
            => Regex.IsMatch(line, SectionRegex);
        
        public static bool IsKeyPar(this string line)
            => Regex.IsMatch(line, KeyParRegex);

        public static string GetSectionName(this string line)
        {
            return new Regex(@"([^\.]+$)")
                .Match(Regex.Replace(line, @"^(\[){1}(.*?)(\]){1}$", "$2"))
                .Value
                .ToString();
        }

        public static KeyValuePair<string, object> GetAttributeNameValue(this string line)
        {
            var matches = Regex
                .Match(line, KeyParRegex)
                .Groups
                .Values
                .ToList();

            return new KeyValuePair<string, object>(
                matches[1].Value ?? string.Empty,
                matches.LastOrDefault().Value ?? new object()
                );
        }
    }
}
