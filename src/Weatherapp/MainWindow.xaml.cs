using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace Weatherapp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DispatcherTimer heartbeatTimer;
        private string lastLocation;
        private WeatherWidget? weatherWidget;
        private DateTime lastUpdateTime = DateTime.MinValue;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Unloaded += MainWindow_Unloaded;
            lastLocation = TxBLocation.Text;

            // Main timer
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(300) };
            timer.Tick += Timer_Tick;
            timer.Start();

            // Heartbeat timer
            heartbeatTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            heartbeatTimer.Tick += HeartbeatTimer_Tick;
            heartbeatTimer.Start();
            Logging.Log("MainWindow initialized and timer started.");
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            InitializeWeatherWidget();
            await FetchWeatherDataIfNeeded(); // Initial data fetch if needed
            Logging.Log("MainWindow loaded and initial weather data fetching started.");
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Logging.Log("MainWindow unloaded and timers stopped.");
            timer.Stop();
            heartbeatTimer.Stop();
        }

        private void InitializeWeatherWidget()
        {
            weatherWidget = new WeatherWidget(weatherWebView, TxBLocation);
            Logging.Log("Weather widget initialized.");
        }

        private void HeartbeatTimer_Tick(object sender, EventArgs e)
        {
            Logging.Log("Heartbeat check: MainWindow is responsive.");
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            Logging.Log("Timer tick occurred.");
            if (TxBLocation.Text != lastLocation)
            {
                Logging.Log($"Location changed from {lastLocation} to {TxBLocation.Text}.");
                lastLocation = TxBLocation.Text;
                await FetchWeatherDataIfNeeded();
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
                    lastUpdateTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Logging.LogException("Error fetching weather data", ex);
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
                    Logging.Log("Weather display updated.");
                }
                catch (Exception ex)
                {
                    Logging.LogException("Failed to update weather display", ex);
                    MessageBox.Show("Failed to update weather display.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private bool ShouldFetchData()
        {
            // Limits data fetching to once per hour or on significant location change
            return DateTime.Now.Subtract(lastUpdateTime).TotalHours >= 1;
        }
    }
}
