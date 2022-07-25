namespace Proxfield.Text.Ini
{
    public class IniFile
    {
        public List<IniSection> Sections;
        public IniFile()
        {
            this.Sections = new List<IniSection>();
        }
        public IniFile(List<IniSection> sections)
        {
            Sections = sections;
        }
    }
}
