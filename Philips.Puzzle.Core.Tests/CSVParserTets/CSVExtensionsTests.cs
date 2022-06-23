using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Philips.Puzzle.Core.Tests.CSVParserTets
{
    public class CSVExtensionsTests
    {
        [Test]
        public void ReadProperFileReadCorrect()
        {
            var csv = "Assets\\soccer.txt"
                .ReadCSVFromPath();

            Assert.True(!string.IsNullOrWhiteSpace(csv));
            Assert.True(csv.Length == 1299);
        }

        [Test]
        public void FileNotExistsThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => "foo".ReadCSVFromPath());
        }

        [Test]
        public void TrivialModelParsedCorrectly()
        {
            var model = "Team \n Foo".ToModel<SocerTeamData>(" ");

            Assert.NotNull(model);
            Assert.True(model.Count() == 1);
            Assert.True(model.First().Name == "Foo");
        }
    }
}
