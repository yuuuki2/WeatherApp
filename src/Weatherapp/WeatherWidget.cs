using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;


namespace Weatherapp
{
    internal class WeatherWidget
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
            webView.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.example", "", CoreWebView2HostResourceAccessKind.DenyCors);
            webView.Source = new Uri("https://appassets.example/WidgetWetter.html");
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string newUrl = e.TryGetWebMessageAsString();
            if (IsUrlBlocked(newUrl))
            {
                return;
            }

            webView.CoreWebView2.Navigate(newUrl);
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            string newUrl = e.Uri;
            if (IsUrlBlocked(newUrl))
            {
                return;
            }

            if (newUrl.Contains("/woche/index/"))
            {
                newUrl = newUrl.Replace("/woche/index/", "/widget/three/");
            }

            Application.Current.Dispatcher.Invoke(() => webView.CoreWebView2.Navigate(newUrl));
        }

        private bool IsUrlBlocked(string url)
        {
            string[] blockedUrls = new string[]
            {
                "https://www.meteoblue.com/de/wetter/woche/wien_%c3%96sterreich_2761369?utm_source=weather_widget&utm_medium=linkus&utm_content=three&utm_campaign=Weather%2BWidget",
                "https://www.meteoblue.com/?utm_source=weather_widget&utm_medium=linkus&utm_content=three&utm_campaign=Weather%2BWidget"
            };

            return Array.Exists(blockedUrls, blockedUrl => url == blockedUrl);
        }

    }
}
