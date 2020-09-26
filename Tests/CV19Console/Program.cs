using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        private const string urlData = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(urlData, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        public static IEnumerable<string> GetDataLines()
        {
            using (var dataStream = GetDataStream().Result)
            {
                using (var streamReader = new StreamReader(dataStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        yield return line;
                    }
                }
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(d => DateTime.Parse(d, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string country, string province, int[] count)>GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim(',');
                var country = row[1].Trim(' ', '"');
                var count = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country, province, count);
            } 
        }

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(urlData).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            foreach (var item in GetDataLines())
            {
                Console.WriteLine(item);
            }
        }
    }
}
