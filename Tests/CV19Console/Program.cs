using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        private const string urlData = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var response = client.GetAsync(urlData).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;
        }
    }
}
