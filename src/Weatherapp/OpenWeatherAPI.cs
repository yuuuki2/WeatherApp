using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

public class WeatherData
{
    // Grundlegende Wetterdaten
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public int Pressure { get; set; }
    public double WindSpeed { get; set; }
    public int WindDirection { get; set; }
    public string CloudCover { get; set; }
    public int Visibility { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public string Country { get; set; }
    public string CityName { get; set; }

    // Luftqualitätsdaten
    public int AirQualityIndex { get; set; }
    public double CO { get; set; }
    public double NO { get; set; }
    public double NO2 { get; set; }
    public double O3 { get; set; }
    public double SO2 { get; set; }
    public double PM2_5 { get; set; }
    public double PM10 { get; set; }

    // Vorhersagedaten
    public List<ForecastData> Forecasts { get; set; }
}

public class ForecastData
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public string Description { get; set; }
}

public static class OpenWeatherAPI
{
    private const string ApiKey = "YOUR_API_KEY";
    private const string WeatherUrl = "https://api.openweathermap.org/data/2.5/weather";
    private const string ForecastUrl = "https://api.openweathermap.org/data/2.5/forecast";
    private const string PollutionUrl = "https://api.openweathermap.org/data/2.5/air_pollution";
    private const string GeocodeUrl = "http://api.openweathermap.org/geo/1.0/direct";

    public static string GetFilePath()
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WeatherApp");
        Directory.CreateDirectory(folderPath); // Ensure the directory exists
        return Path.Combine(folderPath, "Data.json");
    }

    public static async Task<WeatherData> FetchWeatherData(string cityName)
    {
        var coordinates = await FetchCoordinates(cityName);
        if (coordinates == null) return null;

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{WeatherUrl}?lat={coordinates.Item1}&lon={coordinates.Item2}&appid={ApiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    WeatherData weatherData = new WeatherData
                    {
                        Temperature = data.main.temp,
                        Humidity = data.main.humidity,
                        Pressure = data.main.pressure,
                        WindSpeed = data.wind.speed,
                        WindDirection = data.wind.deg,
                        CloudCover = data.weather[0].description,
                        Visibility = data.visibility,
                        Sunrise = UnixTimeStampToDateTime((long)data.sys.sunrise),
                        Sunset = UnixTimeStampToDateTime((long)data.sys.sunset),
                        Country = data.sys.country,
                        CityName = data.name
                    };
                    SaveWeatherData(weatherData);
                    return weatherData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
            }
            return null;
        }
    }

    private static async Task<Tuple<double, double>> FetchCoordinates(string cityName)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{GeocodeUrl}?q={cityName}&limit=1&appid={ApiKey}";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    if (data.Count > 0)
                    {
                        double lat = (double)data[0].lat;
                        double lon = (double)data[0].lon;
                        return Tuple.Create(lat, lon);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching coordinates: {ex.Message}");
            }
            return null;
        }
    }

    public static async Task<List<ForecastData>> FetchForecastData(string cityName)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{ForecastUrl}?q={cityName}&appid={ApiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    List<ForecastData> forecasts = new List<ForecastData>();
                    foreach (var item in data.list)
                    {
                        forecasts.Add(new ForecastData
                        {
                            Time = UnixTimeStampToDateTime((long)item.dt),
                            Temperature = (double)item.main.temp,
                            Description = (string)item.weather[0].description
                        });
                    }
                    return forecasts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching forecast data: {ex.Message}");
            }
            return new List<ForecastData>();
        }
    }

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    public static void SaveWeatherData(WeatherData weatherData)
    {
        try
        {
            string filePath = GetFilePath();
            string json = JsonConvert.SerializeObject(weatherData, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Weather data saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving weather data: {ex.Message}");
        }
    }

    public static void SerializeWeatherData(WeatherData weatherData)
    {
        try
        {
            string filePath = GetFilePath();
            string json = JsonConvert.SerializeObject(weatherData, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Weather data serialized successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error serializing weather data: {ex.Message}");
        }
    }

    public static WeatherData DeserializeWeatherData()
    {
        try
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
                return weatherData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing weather data: {ex.Message}");
        }
        return null;
    }
}
