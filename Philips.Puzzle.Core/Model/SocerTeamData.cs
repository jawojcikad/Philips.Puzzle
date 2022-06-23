using Philips.Puzzle.Core.CSV;

namespace Philips.Puzzle.Core.Model
{
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
}
