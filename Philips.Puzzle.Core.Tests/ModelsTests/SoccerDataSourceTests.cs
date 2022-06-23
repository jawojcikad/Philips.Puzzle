namespace Philips.Puzzle.Core.Tests.ModelsTests
{
    public class SoccerDataSourceTests
    {
        [Test]
        public void SoccerModelDataCorrect()
        {
            var teamsData = "Assets\\soccer.txt"
                .ReadCSVFromPath()
                .ToModel<SocerTeamData>(new string[] { "  ", "-" });

            Assert.IsNotNull(teamsData);
            Assert.That(20 == teamsData.Count());

            Assert.That(teamsData.Any(x => x.Name!.Contains("Ipswich")));
            Assert.That(teamsData.First(x => x.Name!.Contains("Ipswich")).ScoresForTeam == 41);
            Assert.That(teamsData.First(x => x.Name!.Contains("Ipswich")).ScoresAgainstTeam == 64);

            Assert.That(teamsData.Any(x => x.Name!.Contains("Tottenham")));
            Assert.That(teamsData.First(x => x.Name!.Contains("Tottenham")).ScoresForTeam == 49);
            Assert.That(teamsData.First(x => x.Name!.Contains("Tottenham")).ScoresAgainstTeam == 53);
        }
    }
}
