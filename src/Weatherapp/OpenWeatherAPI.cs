using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weatherapp
{
    class OpenWeatherAPI
    {
        public class Weather
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class MainData
        {
            public double TempInKelvin { get; set; }
            public double FeelsLikeInKelvin { get; set; }
            public int Pressure { get; set; }
            public int Humidity { get; set; }
        }

        private const string ApiKey = "04e6b4a61db8e1045007e838910a6321";
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public static async Task<dynamic> GetWeatherDataAsync(string location)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"{BaseUrl}?q={location}&appid={ApiKey}";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<dynamic>(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                    return null;
                }
            }
        }
    }
}
