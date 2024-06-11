using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Newtonsoft.Json;
using System.Text.Json;
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

public class OpenWeatherAPI
{
    private const string ApiKey = "04e6b4a61db8e1045007e838910a6321";
    private const string WeatherUrl = "https://api.openweathermap.org/data/2.5/weather";
    private const string ForecastUrl = "https://api.openweathermap.org/data/2.5/forecast";
    private const string PollutionUrl = "https://api.openweathermap.org/data/2.5/air_pollution";
    private const string GeocodeUrl = "http://api.openweathermap.org/geo/1.0/direct";

    public static async Task<WeatherData> FetchWeatherData(string cityName)
    {
        Logging.Log("Heartbeat: Fetching weather data.");
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Geocode city name to coordinates
                var coords = await GetCoordinatesForCity(client, cityName);
                if (coords == null) return null;

                // Fetch weather data
                string weatherUrl = $"{WeatherUrl}?lat={coords.Item1}&lon={coords.Item2}&appid={ApiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(weatherUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    var weatherData = new WeatherData
                    {
                        Temperature = (double)data.main.temp,
                        Humidity = (int)data.main.humidity,
                        Pressure = (int)data.main.pressure,
                        WindSpeed = (double)data.wind.speed,
                        WindDirection = (int)data.wind.deg,
                        CloudCover = data.weather[0].description,
                        Visibility = (int)data.visibility,
                        Sunrise = UnixTimeStampToDateTime(data.sys.sunrise),
                        Sunset = UnixTimeStampToDateTime(data.sys.sunset),
                        Country = data.sys.country,
                        CityName = data.name,
                        Forecasts = await FetchForecastData(cityName)
                    };

                    // Fetch air pollution data
                    string pollutionUrl = $"{PollutionUrl}?lat={coords.Item1}&lon={coords.Item2}&appid={ApiKey}";
                    response = await client.GetAsync(pollutionUrl);
                    json = await response.Content.ReadAsStringAsync();
                    dynamic pollutionData = JsonConvert.DeserializeObject(json);

                    if (pollutionData.list.Count > 0)
                    {
                        var components = pollutionData.list[0].components;
                        weatherData.AirQualityIndex = (int)pollutionData.list[0].main.aqi;
                        weatherData.CO = (double)components.co;
                        weatherData.NO = (double)components.no;
                        weatherData.NO2 = (double)components.no2;
                        weatherData.O3 = (double)components.o3;
                        weatherData.SO2 = (double)components.so2;
                        weatherData.PM2_5 = (double)components.pm2_5;
                        weatherData.PM10 = (double)components.pm10;
                    }

                    return weatherData;
                }
            }
            catch (Exception ex)
            {
                Logging.LogException("Error fetching weather data", ex);
            }
            return null;
        }
    }

    private static async Task<Tuple<double, double>?> GetCoordinatesForCity(HttpClient client, string cityName)
    {
        string url = $"{GeocodeUrl}?q={cityName}&appid={ApiKey}&limit=1";
        var response = await client.GetAsync(url);
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
        return null;
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
                            Time = UnixTimeStampToDateTime(item.dt),
                            Temperature = item.main.temp,
                            Description = item.weather[0].description
                        });
                    }
                    return forecasts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }
            return new List<ForecastData>();
        }
    }

    private static DateTime UnixTimeStampToDateTime(dynamic unixTimeStamp)
    {
        if (unixTimeStamp == null)
        {
            Logging.Log("Unix timestamp is null, returning DateTime.MinValue");
            return DateTime.MinValue;
        }

        if (double.TryParse(unixTimeStamp.ToString(), out double timeStamp))
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp).ToLocalTime();
            return dateTime;
        }
        else
        {
            Logging.Log($"Invalid unix timestamp: {unixTimeStamp}");
            return DateTime.MinValue;
        }
    }

    /*
    public string Serialize(WeatherData weatherData)
    {
        return $"{weatherData.Temperature},{weatherData.Humidity},{weatherData.Pressure},{weatherData.WindSpeed},{weatherData.WindDirection},{weatherData.CloudCover},{weatherData.Visibility},{weatherData.Sunrise},{weatherData.Sunset},{weatherData.Country},{weatherData.CityName},{weatherData.AirQualityIndex},{weatherData.CO},{weatherData.NO},{weatherData.NO2},{weatherData.O3},{weatherData.SO2},{weatherData.PM2_5},{weatherData.PM10}";
    }
    public static OpenWeatherAPI Deserialize(string data)
    {
        var parts = data.Split(',');
        return new WeatherData
        {
            Temperature = double.Parse(parts[0]),
            Humidity = int.Parse(parts[1]),
            Pressure = int.Parse(parts[2]),
            WindSpeed = double.Parse(parts[3]),
            WindDirection = int.Parse(parts[4]),
            CloudCover = parts[5],
            Visibility = int.Parse(parts[6]),
            Sunrise = DateTime.Parse(parts[7]),
            Sunset = DateTime.Parse(parts[8]),
            Country = parts[9],
            CityName = parts[10],
            AirQualityIndex = int.Parse(parts[11]),
            Forecasts =
            CO = double.Parse(parts[12]),
            NO = double.Parse(parts[13]),
            NO2 = double.Parse(parts[14]),
            O3 = double.Parse(parts[15]),
            SO2 = double.Parse(parts[16]),
            PM2_5 = double.Parse(parts[17]),
            PM10 = double.Parse(parts[18])
        };

    }
    */


    public static string SerializeWeatherData(WeatherData weatherData)
    {
        return System.Text.Json.JsonSerializer.Serialize(weatherData);
    }

    public static WeatherData DeserializeWeatherData(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<WeatherData>(json);
    }

    
}
