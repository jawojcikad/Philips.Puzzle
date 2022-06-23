using Philips.Puzzle.Core.CSV;

namespace Philips.Puzzle.Core.Model
{
    public class WeatherMeasurment
    {
        [CSVColumn("Dy")]
        public int Day { get; set; }

        [CSVColumn("MnT")]
        public double MinimumTemperature { get; set; }

        [CSVColumn("MxT")]
        public double MaximumTemperature { get; set; }
    }
}
