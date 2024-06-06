using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

    public static async Task<WeatherData> FetchWeatherData(string cityName)
    {
        Logging.Log("Heartbeat: Fetching weather data.");
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{WeatherUrl}?q={cityName}&appid={ApiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(url);
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
                        AirQualityIndex = data.air_quality?.aqi ?? 0,  // Verwende den Null-Coalescing-Operator, um Standardwerte zu setzen
                        CO = data.air_quality?.co ?? 0,
                        NO = data.air_quality?.no ?? 0,
                        NO2 = data.air_quality?.no2 ?? 0,
                        O3 = data.air_quality?.o3 ?? 0,
                        SO2 = data.air_quality?.so2 ?? 0,
                        PM2_5 = data.air_quality?.pm2_5 ?? 0,
                        PM10 = data.air_quality?.pm10 ?? 0,
                        Forecasts = await FetchForecastData(cityName)
                    };
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

}

