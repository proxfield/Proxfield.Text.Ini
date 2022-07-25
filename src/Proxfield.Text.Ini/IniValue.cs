namespace Proxfield.Text.Ini
{
    public class IniValue
    {
        private const string OPERATOR = "{0}={1}";
        private KeyValuePair<string, object?> _valuePar;
        private Type? Type;
        public string Name => _valuePar.Key;
        public object? Value => _valuePar.Value;
        public IniValue(KeyValuePair<string, object?> keyValuePair)
        {
            _valuePar = keyValuePair;
        }
        public IniValue(string name, object? value)
        {
            _valuePar = new KeyValuePair<string, object?>(name, value);
        }

        public void Set<T>(string name, T value)
        {
            _valuePar = new KeyValuePair<string, object?>(name, value);
            Type = typeof(T);
        }

        public override string ToString()
            => string.Format(OPERATOR, Name, Value?.ToString());
    }
}