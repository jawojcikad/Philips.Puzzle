## Draft solution for puzzle

All was written in way that
- Maximizes simplicity (LINQ like style, short code)
- Maximizes flexibility (configurable separators, support for custom loggers, ability to have custom converters, models)
- Minimizes cost of maientance (short and simple core code, high test coverage for regression/understanding)

### CSV Library

Simple CSV library (~80 lines) was written from scrath. It is stateless and extension based to work well with LINQ syntax. Solution for soccer library in it would be just 1 line of code

```c#
    string result = args[0]
            .ReadCSVFromPath()
            .ToModel<SocerTeamData>(new string[] { "  ", "-" })
            .OrderBy(x => Math.Abs(x.ScoresForTeam - x.ScoresAgainstTeam))
            .First()
            .HumanRedableName;
```

#### Reading file

You may read data file just by using ``ReadSCVFromPath()`` extension on patch string. It will return you string with file content.

For example:

```c#
var csv = myFilePatch.ReadSCVFromPath();
```

### Parsing CSV

To parse CSV you need to prepare Model that will be used for mapping. It is very simple just add your properties and for properties that need to be filled from CSV file use attribute. For example:

```c#
public class SocerTeamData
    {
        [CSVColumn("Team")]
        public string? Name { get; set; }

        [CSVColumn("F")]
        public int ScoresForTeam { get; set; }

        [CSVColumn("A")]
        public int ScoresAgainstTeam { get; set; }

        public string HumanRedableName
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(Name) && Name.Contains('.'))
                    return Name.Split('.')[1];

                return string.Empty;
            }
        }
    }
```

Contains 3 properties that will be mapped from file described via annotation and one invisible for library.

