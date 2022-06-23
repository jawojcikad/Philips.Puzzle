using System.Globalization;

namespace Philips.Puzzle.Core.Tests.CSVParserTets
{
    public class DoubleWithTextToDoubleConverterTests
    {
        [Test]
        public void ConvertEmptyStringsThrowsException()
        {
            DoubleWithTextToDoubleConverter converter = new DoubleWithTextToDoubleConverter();

            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
            Assert.Throws<FormatException>(() => converter.Convert(string.Empty));
            Assert.Throws<FormatException>(() => converter.Convert(" "));
            Assert.Throws<FormatException>(() => converter.Convert("  "));
        }


        [Test]
        public void ConvertProperStringsGivesCorectResult()
        {
            DoubleWithTextToDoubleConverter converter = new DoubleWithTextToDoubleConverter();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("EN-en");

            Assert.True((double)converter.Convert("1") == 1);
            Assert.True((double)converter.Convert("foo15bar") == 15);
            Assert.True((double)converter.Convert("3.14*") == 3.14);
            Assert.True((double)converter.Convert("---2.71*") == 2.71);
        }

        [Test]
        public void ConvertInvalidStringsThrowsException()
        {
            DoubleWithTextToDoubleConverter converter = new DoubleWithTextToDoubleConverter();

            Assert.Throws<FormatException>(() => converter.Convert("foo"));
            Assert.Throws<FormatException>(() => converter.Convert("XSAODJS(((("));
            Assert.Throws<FormatException>(() => converter.Convert("----"));
        }
    }
}
