using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Weatherapp
{
    public partial class MainWindow : Window
    {
        private WeatherWidget weatherWidget;
        private DispatcherTimer timer;
        private string LastLocation;


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            LastLocation = TxBLocation.Text;
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Big windowww
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            weatherWidget = new WeatherWidget(weatherWebView, TxBLocation);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TxBLocation.Text != LastLocation)
            {
                LastLocation = TxBLocation.Text;
                OpenWeatherAPI.FetchWeatherData(LastLocation);
                UpdateWeatherDisplay();
            }
        }

        private void UpdateWeatherDisplay()
        {
            string cityName = TxBLocation.Text; 
            var weatherData = OpenWeatherAPI.FetchWeatherData(cityName).Result;
            if (weatherData != null)
            {
                Dispatcher.Invoke(() =>
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
                });
            }
            else
            {
                // Fehlerbehandlung, falls weatherData null ist oder unvollständige Daten enthält
                MessageBox.Show("Weather data is not available or incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
