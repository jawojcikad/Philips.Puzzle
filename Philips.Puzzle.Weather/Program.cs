using Philips.Puzzle.Core.CSV;
using Philips.Puzzle.Core.Model;

try
{
    var result = args[0]
        .ReadCSVFromPath()
        .ToModel<WeatherMeasurment>(" ", converters: new List<IValueConverter>() { new DoubleWithTextToDoubleConverter() })
        .OrderBy(x => x.MaximumTemperature - x.MinimumTemperature)
        .First()
        .Day.ToString();

    Console.WriteLine(result);
}
catch (Exception)
{
    Console.WriteLine("Usage: Philips.Puzzle.Weather <path_to_file>");
}
