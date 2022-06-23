using Philips.Puzzle.Core.CSV;
using Philips.Puzzle.Core.Model;

try
{
    string result = args[0]
            .ReadCSVFromPath()
            .ToModel<SocerTeamData>(new string[] { "  ", "-" })
            .OrderBy(x => Math.Abs(x.ScoresForTeam - x.ScoresAgainstTeam))
            .First()
            .HumanRedableName;

    Console.WriteLine(result);
}
catch(Exception)
{
    Console.WriteLine("Usage: Philips.Puzzle.Soccer <path_to_file>");
}
