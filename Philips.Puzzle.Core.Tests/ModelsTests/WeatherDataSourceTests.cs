namespace Philips.Puzzle.Core.Tests.ModelsTests
{
    public class WeatherDataSourceTests
    {
        [Test]
        public void WeatherModelCorrect()
        {
            var measurments = "Assets\\weather.txt"
                .ReadCSVFromPath()
                .ToModel<WeatherMeasurment>(" ", converters: new List<IValueConverter>() { new DoubleWithTextToDoubleConverter() });
            Assert.IsNotNull(measurments);
            Assert.That(measurments.Count() == 30);

            Assert.That(measurments.Any(x => x.Day == 26));
            Assert.That(measurments.First(x => x.Day == 26).MinimumTemperature == 64);
            Assert.That(measurments.First(x => x.Day == 26).MaximumTemperature == 97);

            Assert.That(measurments.Any(x => x.Day == 1));
            Assert.That(measurments.First(x => x.Day == 1).MinimumTemperature == 59);
            Assert.That(measurments.First(x => x.Day == 1).MaximumTemperature == 88);
        }
    }
}