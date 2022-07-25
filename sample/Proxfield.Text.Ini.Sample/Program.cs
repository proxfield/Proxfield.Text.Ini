
using Proxfield.Text.Ini;
using Proxfield.Text.Ini.Sample.Models;

var ini = IniSerializer.SerializeObject<User>(new User()
{
    Email = "jose@outlook.com",
    Password = "1234",
    Name = "Jose",
    UserName = "jose",
    Address = new Address()
    {
        City = "Sao Paulo",
        State = "Sao Paulo",
        Street = "St 10",
        Country = new Country()
        {
            Name = "Brazil"
        }
    }
});

Console.WriteLine(ini);

var obj = IniSerializer.DeserializeObject<User>(ini);
var sections = IniSerializer.GetIniFile(ini);

Console.ReadLine();