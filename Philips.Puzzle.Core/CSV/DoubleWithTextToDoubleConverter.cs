using System.Text.RegularExpressions;

namespace Philips.Puzzle.Core.CSV
{
    public class DoubleWithTextToDoubleConverter : IValueConverter
    {
        public Type Type => typeof(double);

        public object Convert(string value) => System.Convert.ToDouble(Regex.Replace(value, @"[^0-9.,]", ""));
    }
}
