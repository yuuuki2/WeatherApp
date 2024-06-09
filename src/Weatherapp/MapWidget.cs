using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.Web;

namespace Weatherapp
{
    public class MapWidget
    {
        private CoreWebView2 webView;
        private string currentMapUrl;
        private const string GeoNamesApiKey = "Oniichan187"; // geoname username = api key

        public MapWidget(CoreWebView2 webView)
        {
            this.webView = webView;
        }

        public async Task Initialize(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                Logging.Log("Location is empty, cannot initialize map.");
                return;
            }

            var coordinates = await GetCoordinatesForCity(location);
            if (coordinates != null)
            {
                string formattedLocation = FormatLocationForUrl(location, coordinates.Value.geocode);
                Logging.Log($"Formatted location for URL: {formattedLocation}"); // Logt formatierten Ort
                UpdateMapLocation(coordinates.Value.lat, coordinates.Value.lon, formattedLocation);
            }
            else
            {
                Logging.Log("Coordinates not found for the specified location.");
            }
        }

        private async Task<(double lat, double lon, string geocode)?> GetCoordinatesForCity(string cityName)
        {
            using (var client = new HttpClient())
            {
                // Verwende GeoNames API, um die geonameId zu erhalten
                string geocodeUrl = $"http://api.geonames.org/searchJSON?q={HttpUtility.UrlEncode(cityName)}&maxRows=1&username={GeoNamesApiKey}";
                var response = await client.GetAsync(geocodeUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    Logging.Log($"Geocode API response: {json}"); // logt API-Antwort
                    if (data.geonames.Count > 0)
                    {
                        double lat = (double)data.geonames[0].lat;
                        double lon = (double)data.geonames[0].lng;
                        string geocode = (string)data.geonames[0].geonameId;
                        return (lat, lon, geocode);
                    }
                }
            }
            return null;
        }

        private void UpdateMapLocation(double lat, double lon, string formattedLocation)
        {
            currentMapUrl = $"https://www.meteoblue.com/de/wetter/maps/widget/{formattedLocation}?windAnimation=1&gust=1&satellite=1&cloudsAndPrecipitation=1&temperature=1&sunshine=1&extremeForecastIndex=1&geoloc=fixed&tempunit=C&windunit=km%2Fh&lengthunit=metric&zoom=5&autowidth=auto";
            webView.Navigate(currentMapUrl);
            Logging.Log("Map loaded with URL: " + currentMapUrl);
        }

        // Konvertiert den Standortnamen in das URL-konforme Format
        private string FormatLocationForUrl(string location, string geocode)
        {
            string city = location.Split(',')[0].Trim();
            string country = location.Split(',')[1].Trim();
            string formattedCity = HttpUtility.UrlEncode(city.Replace(" ", "_")).ToLower();
            string formattedCountry = HttpUtility.UrlEncode(country.Replace(" ", "_")).ToLower();

            return $"{formattedCity}_{formattedCountry}_{geocode}";
        }

        public string GetCurrentMapUrl()
        {
            return currentMapUrl;
        }
    }
}
