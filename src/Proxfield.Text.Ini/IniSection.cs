namespace Proxfield.Text.Ini
{
    public class IniSection
    {
        private const string OPERATOR = "[{0}]";
        private readonly List<IniValue> _values;
        private readonly string _name;

        public IniSection(string name)
        {
            _name = name;
            _values = new List<IniValue>();
        }
        public void Add(IniValue iniKeyPar)
        {
            _values.Add(iniKeyPar);
        }

        public string GetName() => _name;
        public void Clear() => _values.Clear();
        public object? GetValue(string name)
            => _values
            .Where(prop => name.Equals(prop.Name))
            .FirstOrDefault()?
            .Value;
        public override int GetHashCode() => _values.GetHashCode();
        public string Build() => string.Concat(this.ToString(), Environment.NewLine, string.Join(Environment.NewLine, this._values.Select(p => p.ToString())));
        public override string ToString() => string.Format(OPERATOR, _name);
        public void AddItem<T>(string name, T value) => _values.Add(new IniValue(name, value));
    }
}