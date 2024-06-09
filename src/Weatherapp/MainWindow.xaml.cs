using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Weatherapp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DateTime lastUpdateTime = DateTime.MinValue;
        private string lastLocation;

        private MapWidget mapWidget;
        private WeatherWidget weatherWidget;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            lastLocation = TxBLocation.Text;

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            timer.Tick += Timer_Tick;
            timer.Start();
            Logging.Log("MainWindow initialized and timer started.");
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            InitializeWeatherWidget();
            await InitializeMapWidget();
            await FetchWeatherDataIfNeeded();
        }

        private async Task InitializeMapWidget()
        {
            await mapWebView.EnsureCoreWebView2Async(null);
            mapWidget = new MapWidget(mapWebView.CoreWebView2);
            if (!string.IsNullOrEmpty(TxBLocation.Text))
            {
                await mapWidget.Initialize(TxBLocation.Text);
                mapWebView.Source = new Uri(mapWidget.GetCurrentMapUrl()); // Verwende die aktuelle URL der Map
            }
            Logging.Log("Map widget initialized.");
        }

        private void InitializeWeatherWidget()
        {
            weatherWidget = new WeatherWidget(weatherWebView, TxBLocation);
            Logging.Log("Weather widget initialized.");
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (TxBLocation.Text != lastLocation)
            {
                lastLocation = TxBLocation.Text;
                await FetchWeatherDataIfNeeded();
                if (!string.IsNullOrEmpty(TxBLocation.Text))
                {
                    await mapWidget.Initialize(TxBLocation.Text); // Aktualisiere die Map, wenn sich der Standort ändert
                    mapWebView.Source = new Uri(mapWidget.GetCurrentMapUrl()); // Verwende die aktuelle URL der Map
                }
            }
        }

        private async Task FetchWeatherDataIfNeeded()
        {
            if (!string.IsNullOrEmpty(TxBLocation.Text) && ShouldFetchData())
            {
                await FetchWeatherDataAsync(TxBLocation.Text);
            }
        }

        private async Task FetchWeatherDataAsync(string location)
        {
            try
            {
                var weatherData = await OpenWeatherAPI.FetchWeatherData(location);
                if (weatherData != null)
                {
                    UpdateWeatherDisplay(weatherData);
                    Logging.Log($"Fetched weather data for {location}.");
                }
                else
                {
                    Logging.Log("No data received from API.");
                }
            }
            catch (Exception ex)
            {
                Logging.LogException("Error during API call", ex);
            }
        }

        private void UpdateWeatherDisplay(WeatherData weatherData)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    LbiTemperature.Content = $"Temperature: {weatherData.Temperature} °C";
                    LbiHumidity.Content = $"Humidity: {weatherData.Humidity}%";
                    LbiAirpressure.Content = $"Air pressure: {weatherData.Pressure} hPa";
                    LbiWinddata.Content = $"Wind: {weatherData.WindSpeed} m/s, {weatherData.WindDirection}°";
                    LbiCloudcover.Content = $"Cloud cover: {weatherData.CloudCover}";
                    LbiVisibility.Content = $"Visibility: {weatherData.Visibility} meters";
                    LbiSunrise.Content = $"Sunrise: {weatherData.Sunrise}";
                    LbiSunset.Content = $"Sunset: {weatherData.Sunset}";
                    LbiAQI.Content = $"Air Quality Index: {weatherData.AirQualityIndex}";
                    LbiCO.Content = $"CO: {weatherData.CO} μg/m3";
                    LbiNO.Content = $"NO: {weatherData.NO} μg/m3";
                    LbiNO2.Content = $"NO2: {weatherData.NO2} μg/m3";
                    LbiO3.Content = $"O3: {weatherData.O3} μg/m3";
                    LbiSO2.Content = $"SO2: {weatherData.SO2} μg/m3";
                    LbiPM25.Content = $"PM2.5: {weatherData.PM2_5} μg/m3";
                    LbiPM10.Content = $"PM10: {weatherData.PM10} μg/m3";

                    // Überprüfe, ob Umweltdaten vorhanden sind
                    if (weatherData.AirQualityIndex > 0 || weatherData.CO > 0)
                    {
                        LbiAQI.Content = $"Air Quality Index: {weatherData.AirQualityIndex}";
                        LbiCO.Content = $"CO: {weatherData.CO} μg/m3";
                    }
                    else
                    {
                        LbiAQI.Content = "Air Quality Index: Data not available";
                        LbiCO.Content = "CO: Data not available";
                    }
                }
                catch (Exception ex)
                {
                    Logging.LogException("Failed to update UI with weather data", ex);
                    MessageBox.Show("Failed to update weather display.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private bool ShouldFetchData()
        {
            return DateTime.Now.Subtract(lastUpdateTime).TotalHours >= 1;
        }
    }
}
