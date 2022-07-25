The Ini/Config/Conf reader library is a lightweight .ini file parser for DotNet.

![GitHub License](https://img.shields.io/github/license/proxfield/Proxfield.Text.Ini)
[![Version](https://img.shields.io/badge/version-0.1.0-brightgreen.svg)](https://semver.org)
![Actions](https://github.com/proxfield/Proxfield.Text.Ini/actions/workflows/build.yml/badge.svg)
[![Nuget](https://github.com/proxfield/Proxfield.Text.Ini/actions/workflows/release.yml/badge.svg)](https://github.com/proxfield/Proxfield.Extensions.Caching.SQLite/actions/workflows/release.yml)
![GitHub branch checks state](https://img.shields.io/github/checks-status/proxfield/Proxfield.Text.Ini/main)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/proxfield/Proxfield.Text.Ini)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Proxfield.Text.Ini.svg)](https://www.nuget.org/packages/Proxfield.Text.Ini)

## Nuget
```bash
PM> Install-Package Proxfield.Text.Ini
```

Visit out project at the [Nuget Repository Page](https://www.nuget.org/packages/Proxfield.Text.Ini) to know more.

## How

The Ini files are commom type of document used to store key par values into section, it has wide adoption on confguration files, such as the example:

```ini
; this is just a comentary
[owner]
name = John Doe
organization = Acme Widgets Inc.
```

On the case above, the `owner` is a section. A section is composed of one or more key-par value, in this case `name` and `organization`. 

Complex objects are handled in a tree format way, everytime a section (or object) has inside of it another section (object) it is displayed as `object.propertyObject` format.

```csharp
var user = new User(){
    Email = "jose@outlook.com",
    Password = "1234",
    Name = "Jose",
    UserName = "jose",
    Address = new Address()
    {
        City = "Sao Paulo",
        State = "Sao Paulo",
        Street = "St 10"
    }
}
```

We can see that the `Address` object becames the section `User.Address`, because it is inside the object User.

```ini
[User]
UserId=cd1093f4-395d-460a-b66c-0b62af0e3d17
Name=Jose
Email=jose@outlook.com
Password=1234
UserName=jose
[User.Address]
Street=St 10
City=Sao Paulo
State=Sao Paulo
```

## Usage

Serializing object:
```csharp
string iniContent = IniSerializer.SerializeObject<User>(user);
```
Deserializing object:
```csharp
var user = IniSerializer.DeserializeObject<User>(iniContent);
```

## Platform Support
Porxfield.Text.Ini is compiled for DotNet 6, soon there will versions available for other plataforms.
- [x] DotNet 6
- [ ] DotNet 5

## License
The MIT License (MIT)

Copyright (c) 2022 Proxfield

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
