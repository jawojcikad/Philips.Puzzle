using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace Philips.Puzzle.Core.CSV
{
    public static class CSVExtensions
    {
        public static string ReadCSVFromPath(this string path)
        {
            using var reader = new StreamReader(path);
            return reader.ReadToEnd();
        }

        public static IEnumerable<T> ToModel<T>(this string csv, string columnSeparator, ILogger logger = null, List<IValueConverter> converters = null) 
            => csv.ToModel<T>(new string[] { columnSeparator }, logger, converters);

        public static IEnumerable<T> ToModel<T>(this string csv, string[] columnSeparators, ILogger? logger = null, List<IValueConverter> converters = null)
        {
            var separators = new string[] { Environment.NewLine, "\n" };
            var lines = csv.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var propertiesToCSVIndexes = ParseHeader<T>(lines[0], columnSeparators);

            var result = new List<T>();

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    var columnValues = lines[i].Split(columnSeparators, StringSplitOptions.RemoveEmptyEntries);
                    result.Add(GetModel<T>(columnValues, propertiesToCSVIndexes, converters));
                }
                catch (Exception ex)
                {
                    var message = $"Error importing Line {i + 1} of {lines.Length}. Line content '{lines[i]}'. Details of error: {ex.Message} ";
                    if (logger != null)
                        logger.Log(LogLevel.Warning, message);
                    else
                        Trace.WriteLine(message);
                }
            }

            return result;
        }

        private static T GetModel<T>(string[] line, Dictionary<int, PropertyInfo> propertiesToCSVIndexes, List<IValueConverter> converters = null)
        {
            var model = Activator.CreateInstance<T>();

            foreach (int key in propertiesToCSVIndexes.Keys)
            {
                var propertyInfo = propertiesToCSVIndexes[key];
                try
                {
                    object value = converters != null && converters.Any(x=> x.Type == propertyInfo.PropertyType) ?
                        converters.First(x=> x.Type == propertyInfo.PropertyType).Convert(line[key]) : 
                        Convert.ChangeType(line[key], propertyInfo.PropertyType);

                    propertyInfo.SetValue(model, value, null);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Value '{line[key]}' couldn't be converted to '{propertyInfo.PropertyType}'", ex);
                }
            }

            return model;
        }

        private static Dictionary<int, PropertyInfo> ParseHeader<T>(string headerLine, string[] columnSeparators)
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();

            var headers = headerLine.Split(columnSeparators, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            
            return properties.Where(x => x.GetCustomAttributes(typeof(CSVColumnAttribute), false).FirstOrDefault() != null)
                .Where(x=> Array.IndexOf(headers, (x.GetCustomAttributes(typeof(CSVColumnAttribute), false).First() as CSVColumnAttribute).Name) != -1)
                .Select(x => new KeyValuePair<int, PropertyInfo>(Array.IndexOf(headers, (x.GetCustomAttributes(typeof(CSVColumnAttribute), false).First() as CSVColumnAttribute).Name), x))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
