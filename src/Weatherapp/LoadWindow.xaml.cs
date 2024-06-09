using System;
using System.IO;
using System.Windows;

namespace Weatherapp
{
    /// <summary>
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        public LoadWindow()
        {
            InitializeComponent();
            LoadWeatherData();
        }

        private void LoadWeatherData()
        {
            try
            {
                // Pfad zur JSON-Datei
                string filePath = OpenWeatherAPI.GetFilePath();

                // Überprüfen, ob die Datei existiert
                if (File.Exists(filePath))
                {
                    // Deserialisiere den JSON-String in ein WeatherData-Objekt
                    WeatherData weatherData = OpenWeatherAPI.DeserializeWeatherData();

                    // Überprüfen, ob Daten erfolgreich deserialisiert wurden
                    if (weatherData != null)
                    {
                        // Anzeigen der Daten im ListBox
                        DisplayWeatherData(weatherData);
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Deserialisieren der Daten.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Die Datei existiert nicht.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Wetterdaten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayWeatherData(WeatherData weatherData)
        {
            // Anzeigen der Wetterdaten im ListBox
            LbiTemperature.Content = $"Temperature: {weatherData.Temperature} °C";
            LbiHumidity.Content = $"Humidity: {weatherData.Humidity}%";
            LbiAirpressure.Content = $"Air pressure: {weatherData.Pressure} hPa";
            LbiWinddata.Content = $"Wind direction/speed: {weatherData.WindDirection}/{weatherData.WindSpeed} m/s";
            LbiCloudcover.Content = $"Cloud cover: {weatherData.CloudCover}";
            LbiVisibility.Content = $"Visibility: {weatherData.Visibility} meters";
            LbiSunrise.Content = $"Sunrise: {weatherData.Sunrise}";
            LbiSunset.Content = $"Sunset: {weatherData.Sunset}";
            LbiAQI.Content = $"Air Quality Index: {weatherData.AirQualityIndex}";
            LbiCO.Content = $"CO (Carbon Monoxide): {weatherData.CO} μg/m3";
            LbiNO.Content = $"NO (Nitrogen Monoxide): {weatherData.NO} μg/m3";
            LbiNO2.Content = $"NO2 (Nitrogen Dioxide): {weatherData.NO2} μg/m3";
            LbiO3.Content = $"O3 (Ozone): {weatherData.O3} μg/m3";
            LbiSO2.Content = $"SO2 (Sulphur Dioxide): {weatherData.SO2} μg/m3";
            LbiPM25.Content = $"PM2.5: {weatherData.PM2_5} μg/m3";
            LbiPM10.Content = $"PM10: {weatherData.PM10} μg/m3";
        }
    }
}
