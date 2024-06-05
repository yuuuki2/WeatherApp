using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Weatherapp
{
    public class WeatherWidget
    {
        private WebView2 webView;
        private TextBlock TxBLocation;
        private DispatcherTimer heartbeatTimer;

        public WeatherWidget(WebView2 webViewControl, TextBlock textBlockLocation)
        {
            heartbeatTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            heartbeatTimer.Tick += HeartbeatTimer_Tick;
            heartbeatTimer.Start();

            Logging.Log("WeatherWidget initialized and Heartbeat timer started.");

            webView = webViewControl;
            TxBLocation = textBlockLocation;
            InitializeWebView();
        }

        private void HeartbeatTimer_Tick(object sender, EventArgs e)
        {
            Logging.Log("Heartbeat check: WeatherWidget is responsive.");
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);
                webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
                webView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
                webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                webView.Source = new Uri("https://www.meteoblue.com/de/wetter/widget/three/wien_%c3%96sterreich_2761369");
                Logging.Log("WebView initialized successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Logging.LogException("Error initializing WebView", ex);
            }
        }


        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string newUrl = e.TryGetWebMessageAsString();
            HandleUrlNavigation(newUrl);
        }

        private void CoreWebView2_NewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            string newUrl = e.Uri;
            HandleUrlNavigation(newUrl);
        }

        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            string newUrl = e.Uri;
            if (IsUrlBlocked(newUrl))
            {
                e.Cancel = true;
                return;
            }
        }

        private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                try
                {
                    await Task.Delay(500);
                    string script = "document.querySelector('input.location').getAttribute('placeholder');";
                    string content = await webView.CoreWebView2.ExecuteScriptAsync(script);
                    string decodedContent = System.Text.Json.JsonSerializer.Deserialize<string>(content);
                    Application.Current.Dispatcher.Invoke(() => TxBLocation.Text = decodedContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during NavigationCompleted: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void HandleUrlNavigation(string url)
        {
            if (IsUrlBlocked(url))
            {
                return;
            }

            string finalUrl = url.Contains("/wetter/woche/index/") ?
                Regex.Replace(url, "/wetter/woche/index/", "/wetter/widget/three/") : url;

            Application.Current.Dispatcher.Invoke(() => webView.CoreWebView2.Navigate(finalUrl));
        }

        private bool IsUrlBlocked(string url)
        {
            string[] blockedPatterns = new string[]
            {
                @"https:\/\/www\.meteoblue\.com\/\?utm_source=weather_widget&utm_medium=linkus&utm_content=three&utm_campaign=Weather%2BWidget",
                @"https:\/\/www\.meteoblue\.com\/de\/wetter\/woche\/(?!index\/).*\?utm_source=weather_widget&utm_medium=linkus&utm_content=three&utm_campaign=Weather%2BWidget",
                @"https:\/\/www\.meteoblue\.com\/de\/wetter\/woche\/wien_%c3%96sterreich_2761369"
            };

            foreach (var pattern in blockedPatterns)
            {
                if (Regex.IsMatch(url, pattern))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
