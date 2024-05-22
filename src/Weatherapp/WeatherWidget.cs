using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Text.RegularExpressions;

namespace Weatherapp
{
    public class WeatherWidget
    {
        private WebView2 webView;

        public WeatherWidget(WebView2 webViewControl)
        {
            webView = webViewControl;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            webView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
            webView.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.example", "", CoreWebView2HostResourceAccessKind.DenyCors);
            webView.Source = new Uri("https://appassets.example/WidgetWetter.html");
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string newUrl = e.TryGetWebMessageAsString();
            HandleUrlNavigation(newUrl);
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
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

        private void HandleUrlNavigation(string url)
        {
            if (IsUrlBlocked(url))
            {
                // Blockiere die Navigation, indem du nichts machst
                return;
            }

            // Ersetze die URL, um sicherzustellen, dass sie korrekt angezeigt wird
            if (url.Contains("/wetter/woche/index/"))
            {
                string newUrl = Regex.Replace(url, "/wetter/woche/index/", "/wetter/widget/three/");
                Application.Current.Dispatcher.Invoke(() => webView.CoreWebView2.Navigate(newUrl));
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => webView.CoreWebView2.Navigate(url));
            }
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
