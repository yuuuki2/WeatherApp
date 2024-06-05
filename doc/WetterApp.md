# Wetter APP

* **Must Haves:**

  * [Weather API](https://openweathermap.org)
    * City search system
    * Displays:
      * Country
      * Temp
      * Clouds
      * Humidity
      * Pressure
      * Wind Direction
      * Windspeed
  * Map with the Data (the normal thing)
  * Go back the last three days
  * it will save nature disastars (if the WeatherAPI have it{like Storms, Hurrycans...}) or the hottest and coldes days in a csv or json
  * it will save all favoriten citys inputs form user in a csv or json and is also displayed directly
  * Animated UI/UX
  * show weahter forcast (if possible)
  * loggin
* **Nice to Have:**

  * The hottes and coldes Place on the World today (if possible)
  * More selection of graphics (Kilmadiagramm, and more)
  * map shows data when you click on it
* **Not nice to Haves**

  * get autmatic you location for the current weather in your area
  * Login system (for the their Personal Data)
  * More languages than English

# Plan

Thirst the basic of the Weather App then the "fetures" (not the nice to haves)

# Class

1. For each API that we use, needs a Class
2. load and save Class
3. 

# Division of work

| Name   | What did you do?                                | Date       |
| ------ | ----------------------------------------------- | ---------- |
| Sascha | Added a Checklist and seached for some API´s    | 08.05.2024 |
| Manuel | Test of Map/Display Widgets                     | 18.05.2024 |
| Manuel | Added API Doku and Use                          | 15.05.2024 |
| Sascha | Added a new class and worked on the GUI         | 22.05.2024 |
| Manuel | Worked on WheaterWidget and implementing in GUI | 22.05.2024 |
| Sascha | Added Item List Box with their items            | 29.05.2024 |
| Sascha | Rework the GUI with Tabitem                     | 05.06.2024 |
| Manuel | updatet OpenWeatherAPI.cs, MainWindow.xaml & .cs| 05.06.2024 |
|        |                                                 |            |

>>>>>>> e6ad25b22e0c8a50b3831a1ba2f61699d0f869bb

# Checklist

* [x] the basic of the Weather App (search and find)
* [x] Finish the GUI
* [ ] All Must Haves are done
* [ ] Polish the Programm

# API's

## Nutzungsempfehlungen

### Funktionen exklusiv für ECMWF (1):

1. **Reanalysen und Vorhersagen:**
   - Temperatur
   - Niederschlag
   - Windgeschwindigkeit und -richtung
   - Luftdruck
   - Feuchtigkeit
   - Wolkendecke
   - Strahlung
   - Bodenfeuchte
   - CAPE (Convective Avalible Pontentian Energy)
   - Lifted Index
   - Grenzschichthöhe
   - Helizität
   - Konvektive Hemmung
   - Wolkenwasser und -eis
   - Höhe der Gefriergrenze

### Funktionen exklusiv für WeatherAPI (2):

1. **Geocoding API:**
   - Konvertiert Ortsnamen in geografische Koordinaten und umgekehrt.
   - Beispiel: Suche nach den Koordinaten einer Stadt oder finde den Städtenamen basierend auf Koordinaten.

### Funktionen exklusiv für Meteoblue (3):

1. **Agronomical Packages:**

   - **Agro:**
     - Oberflächen-, Taupunkt- und Nasskugeltemperatur
     - Gesamte, potenzielle und ET0-Referenzverdunstung
     - Blattnässeindex
     - Bodentemperatur und -feuchtigkeit
     - Mittlerer fühlbarer Wärmefluss
   - **Agromodel Leaf Wetness:**
     - Blattnässe-Regenindex
     - Blattnässe-Tauindex
     - Blattnässe-Wahrscheinlichkeit
     - Blattnässe-Verdunstungsindex
   - **Agromodel Sowing:**
     - Verfügbare Kulturen: Mais, Weizen, Gerste, Raps, Kartoffel, Zuckerrüben, Sojabohne, Baumwolle, Reisindica, Reijaponi, Sorghum
   - **Agromodel Spray:**
     - Sprühfenster
2. **Renewable Energy Packages:**

   - **Solar:**
     - Global horizontale Bestrahlungsstärke (GHI)
     - Diffuse Strahlung (DIF)
     - Direkte normale Bestrahlungsstärke (DNI)
     - Globale normale Bestrahlungsstärke (GNI)
     - Extraterrestrische Sonnenstrahlung
   - **Solar Ensemble:**
     - Max GHI rückwärts
     - Min GHI rückwärts
     - GHI rückwärts Konsens
     - GHI rückwärts p90 Überschreitung
   - **Wind:**
     - Windböen in 10m Höhe
     - Windrichtung in 80m Höhe
     - Windgeschwindigkeit in 80m Höhe
     - Luftdichte
     - Luftdruck
   - **Wind 80m Ensemble:**
     - Konsens der Windgeschwindigkeit in 80m Höhe
     - p90 Überschreitung der Windgeschwindigkeit in 80m Höhe
     - Min und Max Windgeschwindigkeit in 80m Höhe
3. **Advanced Packages:**

   - **Sea:**
     - Signifikante Höhe, mittlere Periode, Spitzenwellenperiode der Dünungswellen
     - Windwellenhöhe, -richtung und -spitzen
     - Meerestemperatur an der Oberfläche
     - Mittlere Windwellenperiode
     - Mittlere Wellenperiode und -richtung
     - Aktuelle Geschwindigkeit U und V
     - Signifikante Wellenhöhe
   - **Air:**
     - CAPE
     - Lifted Index
     - Höhe der Grenzschicht
     - Helizität
     - Konvektive Hemmung
     - Wolkenwasser und -eis
     - Höhe der Gefriergrenze
   - **Air Quality:**
     - Luftqualitätsindex
     - Birken-, Gras-, Olivenpollen
     - PM2.5, PM10
     - SO2, NO2, CO
     - Ozon, Staub und AOD500 Konzentration
     - Sandsturmwarnung
4. **Multimodel Packages:**

   - Temperatur
   - Windgeschwindigkeit und -richtung
   - Niederschlag
   - Wolkendecke
   - Temperatur und Windgeschwindigkeit mit Streuung
   - Kurzwellige Strahlung (GHI)

### Funktionen exklusiv für OpenWeatherMap (4):

1. **Minute Forecast for 1 Hour**: Bereitstellung von minütlichen Wetterdaten für die nächste Stunde, ideal für kurzfristige Entscheidungen.
2. **Hourly Forecast for 48 Hours**: Stündliche Wettervorhersagen für die nächsten zwei Tage, um detaillierte Planungen zu unterstützen.
3. **Daily Forecast for 8 Days**: Tägliche Wettervorhersagen, die acht Tage in die Zukunft reichen, um langfristigere Vorhaben zu erleichtern.
4. **Government Weather Alerts**: Integration von amtlichen Wetterwarnungen, die es Benutzern ermöglicht, über wichtige Wetterereignisse informiert zu bleiben.
5. **Weather Data for Any Timestamp**: Zugriff auf Wetterdaten für jeden gewünschten Zeitpunkt, von historischen Daten bis zu Vorhersagen vier Tage im Voraus.
6. **Daily Aggregation of Weather Data**: Tägliche Zusammenfassungen des Wetters, verfügbar von einem Archiv, das 45 Jahre zurückreicht, bis zu 1,5 Jahren in die Zukunft.
7. **Weather Overview with AI Technologies**: Nutzung von KI-Technologien von OpenWeather, um eine leicht verständliche Zusammenfassung des aktuellen und des morgigen Wetters zu bieten.
8. **Proprietary OpenWeather Model**: Die API basiert auf dem proprietären Modell von OpenWeather, das alle 10 Minuten aktualisiert wird, um genaue und aktuelle Wetterdaten zu liefern.


### Gemeinsame Funktionen, die von ECMWF genutzt werden können (ohne Limit):

- **Weather Maps API (OpenWeatherMap, meteoblue):**

  - Stellt Wetterkarten zur Verfügung, die meteorologische Parameter wie Temperatur, Niederschlag, Wind und Wolkenabdeckung als Overlays auf einer Karte darstellen.
  - Erstelle dynamische Wetterkarten für eine Website oder Anwendung.
- **5 Day / 3 Hour Forecast API (OpenWeatherMap, meteoblue):**

  - Liefert Wettervorhersagen in 3-Stunden-Intervallen für die nächsten 5 Tage.
  - Erhalte präzise Wettervorhersagen für die nächsten Tage zur Planung von Aktivitäten.
- **Current Weather Data API (OpenWeatherMap, meteoblue):**

  - Stellt aktuelle Wetterdaten für einen bestimmten Ort bereit.
  - Zeige das aktuelle Wetter basierend auf der aktuellen Position des Benutzers oder einer festgelegten Stadt an.

## Detaillierte API-Beschreibungen

### OpenWeatherMap APIs

1,000 API calls per day for free

#### 1. [Geocoding API](https://openweathermap.org/api/geocoding-api)

- **Funktion:** Konvertiert Ortsnamen in geografische Koordinaten (Breitengrad und Längengrad) und umgekehrt.
- **Anwendungsbeispiel:** Suche nach den Koordinaten einer Stadt oder finde den Städtenamen basierend auf Koordinaten.

#### 2. [Air Pollution API](https://openweathermap.org/api/air-pollution)

- **Funktion:** Bietet aktuelle, historische und Vorhersagedaten zur Luftverschmutzung.
- **Anwendungsbeispiel:** Überwache die Luftqualität in einer bestimmten Stadt und erhalte Warnungen bei hohen Schadstoffwerten.

#### 3. [Weather Maps API](https://openweathermap.org/api/weathermaps)

- **Funktion:** Stellt Wetterkarten zur Verfügung, die meteorologische Parameter wie Temperatur, Niederschlag, Wind und Wolkenabdeckung als Overlays auf einer Karte darstellen.
- **Anwendungsbeispiel:** Erstelle dynamische Wetterkarten für eine Website oder Anwendung.

#### 4. [5 Day / 3 Hour Forecast API](https://openweathermap.org/forecast5)

- **Funktion:** Liefert Wettervorhersagen in 3-Stunden-Intervallen für die nächsten 5 Tage.
- **Anwendungsbeispiel:** Erhalte präzise Wettervorhersagen für die nächsten Tage zur Planung von Aktivitäten.

#### 5. [Current Weather Data API](https://openweathermap.org/current)

- **Funktion:** Stellt aktuelle Wetterdaten für einen bestimmten Ort bereit.
- **Anwendungsbeispiel:** Zeige das aktuelle Wetter basierend auf der aktuellen Position des Benutzers oder einer festgelegten Stadt an.

### meteoblue APIs

10.000.000 Credits

#### [Overview](https://docs.meteoblue.com/en/weather-apis/introduction/overview)

[Availability](https://docs.meteoblue.com/en/weather-apis/introduction/availability)

#### [Package API](https://docs.meteoblue.com/en/weather-apis/packages-api/overview)

[Forecast Data](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data)
[Forecast Data V2 FAQ](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data-v2-faq)
[History &amp; Climate Data](https://docs.meteoblue.com/en/weather-apis/packages-api/history-and-climate-data)
[Weather Warnings](https://docs.meteoblue.com/en/weather-apis/packages-api/weather-warnings)

#### 1. General Forecast Packages

##### [Basic](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#basic)

- **Funktion:** Für allgemeine Zwecke, enthält die gängigsten Wettervariablen.
- **Verfügbare Intervalle:**
  - basic-15min (8000 Credits)
  - basic-1h (8000 Credits)
  - basic-3h (8000 Credits)
  - basic-day (4000 Credits)
- **Wettervariablen:**
  - Temperatur
  - Schneefraktion
  - Windgeschwindigkeit und -richtung
  - Relative Luftfeuchtigkeit
  - Luftdruck auf Meereshöhe
  - Piktogrammcode
  - Konvektive Niederschläge
  - Gefühlte Temperatur
  - Niederschlagsmenge und Wahrscheinlichkeit
  - rainSPOT

##### [Current](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#current)

- **Funktion:** Aktuelle Wetterinformationen, einschließlich Beobachtungen und Messungen.
- **Verfügbare Intervalle:**
  - current (4000 Credits)
- **Wettervariablen:**
  - Temperatur
  - Windgeschwindigkeit
  - Piktogrammcode
  - Wolken

##### [Clouds](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#clouds)

- **Funktion:** Detaillierte Wolkenschichten- und Sonnenscheindauerprognosen.
- **Verfügbare Intervalle:**
  - clouds-1h (4000 Credits)
  - clouds-3h (4000 Credits)
  - clouds-day (2000 Credits)
- **Wettervariablen:**
  - Sichtweite
  - Sonnenscheindauer
  - Niedrige, mittlere, hohe und totale Wolkendecke

##### [Sun and Moon](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#sun-and-moon)

- **Funktion:** Informationen über Auf- und Untergangszeiten von Sonne und Mond.
- **Verfügbare Intervalle:**
  - sunmoon (2000 Credits)
- **Wettervariablen:**
  - Sonnenaufgangs- und Sonnenuntergangszeit
  - Mondaufgangs- und Monduntergangszeit
  - Mondphasenwinkel und Name
  - Mondalter
  - Mondphasentransitzeit

#### 2. Agronomical Packages

##### [Agro](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#agro)

- **Funktion:** Für landwirtschaftliche Zwecke, enthält Boden- und Vegetationsbezogene Wettervariablen.
- **Verfügbare Intervalle:**
  - agro-1h (8000 Credits)
  - agro-3h (8000 Credits)
  - agro-day (4000 Credits)
- **Wettervariablen:**
  - Oberflächen-, Taupunkt- und Nasskugeltemperatur
  - Gesamte, potenzielle und ET0-Referenzverdunstung
  - Blattnässeindex
  - Bodentemperatur und -feuchtigkeit
  - Mittlerer fühlbarer Wärmefluss

##### [Agromodel Leaf Wetness](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#agromodel-leaf-wetness)

- **Funktion:** Relevante Informationen zur Überwachung und Vorhersage von Blattnässe.
- **Verfügbare Intervalle:**
  - agromodelleafwetness-1h (8000 Credits)
- **Wettervariablen:**
  - Blattnässe-Regenindex
  - Blattnässe-Tauindex
  - Blattnässe-Wahrscheinlichkeit
  - Blattnässe-Verdunstungsindex

##### [Agromodel Sowing](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#agromodel-sowing)

- **Funktion:** Informationen über geeignete Zeitfenster für die Aussaat von Kulturen.
- **Verfügbare Intervalle:**
  - agromodelsowing-1h (4000 Credits)
- **Wettervariablen:**
  - Verfügbare Kulturen: Mais, Weizen, Gerste, Raps, Kartoffel, Zuckerrüben, Sojabohne, Baumwolle, Reisindica, Reijaponi, Sorghum

##### [Agromodel Spray](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#agromodel-spray)

- **Funktion:** Informationen über geeignete Zeitfenster für das Sprühen von Feldern.
- **Verfügbare Intervalle:**
  - agromodelspray-1h (4000 Credits)
- **Wettervariablen:**
  - Sprühfenster

#### 3. Renewable Energy Packages

##### [Solar](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#solar)

- **Funktion:** Spezifische Strahlungsvariablen für den Solarsektor.
- **Verfügbare Intervalle:**
  - solar-15min (16000 Credits)
  - solar-1h (16000 Credits)
  - solar-3h (16000 Credits)
  - solar-day (8000 Credits)
- **Wettervariablen:**
  - Global horizontale Bestrahlungsstärke (GHI)
  - Diffuse Strahlung (DIF)
  - Direkte normale Bestrahlungsstärke (DNI)
  - Globale normale Bestrahlungsstärke (GNI)
  - Extraterrestrische Sonnenstrahlung

##### [Solar Ensemble](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#solar-ensemble)

- **Funktion:** Aggregierte und individuelle Mitglieder des GFS Ensemble Solar Forecast.
- **Verfügbare Intervalle:**
  - solarensemble-1h (16000 Credits)
- **Wettervariablen:**
  - Max GHI rückwärts
  - Min GHI rückwärts
  - GHI rückwärts Konsens
  - GHI rückwärts p90 Überschreitung

##### [Wind](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#wind)

- **Funktion:** Spezifische Windvariablen für den Windenergiesektor.
- **Verfügbare Intervalle:**
  - wind-15min (16000 Credits)
  - wind-1h (16000 Credits)
  - wind-3h (16000 Credits)
  - wind-day (8000 Credits)
- **Wettervariablen:**
  - Windböen in 10m Höhe
  - Windrichtung in 80m Höhe
  - Windgeschwindigkeit in 80m Höhe
  - Luftdichte
  - Luftdruck

##### [Wind 80m Ensemble](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#wind-80m-ensemble)

- **Funktion:** Aggregierte und individuelle Mitglieder des GFS Ensemble Wind Forecast.
- **Verfügbare Intervalle:**
  - wind80ensemble-1h (16000 Credits)
- **Wettervariablen:**
  - Konsens der Windgeschwindigkeit in 80m Höhe
  - p90 Überschreitung der Windgeschwindigkeit in 80m Höhe
  - Min und Max Windgeschwindigkeit in 80m Höhe

#### 4. Advanced Packages

##### [Sea](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#sea)

- **Funktion:** Marine Wettervorhersagen.
- **Verfügbare Intervalle:**
  - sea-1h (4000 Credits)
  - sea-3h (4000 Credits)
  - sea-day (2000 Credits)
- **Wettervariablen:**
  - Signifikante Höhe, mittlere Periode, Spitzenwellenperiode der Dünungswellen
  - Windwellenhöhe, -richtung und -spitzen
  - Meerestemperatur an der Oberfläche
  - Mittlere Windwellenperiode
  - Mittlere Wellenperiode und -richtung
  - Aktuelle Geschwindigkeit U und V
  - Signifikante Wellenhöhe

##### [Air](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#air)

- **Funktion:** Atmosphärische Vorhersagen wie CAPE und Lifted Index.
- **Verfügbare Intervalle:**
  - air-1h (8000 Credits)
  - air-3h (8000 Credits)
  - air-day (4000 Credits)
- **Wettervariablen:**
  - CAPE
  - Lifted Index
  - Höhe der Grenzschicht
  - Helizität
  - Konvektive Hemmung
  - Wolkenwasser und -eis
  - Höhe der Gefriergrenze

##### [Air Quality](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#air-quality)

- **Funktion:** Luftqualitätsvorhersage wie Schadstoffkonzentrationen.
- **Verfügbare Intervalle:**
  - airquality-1h (8000 Credits)
  - airquality-3h (8000 Credits)
  - airquality-day (4000 Credits)
- **Wettervariablen:**
  - Luftqualitätsindex
  - Birken-, Gras-, Olivenpollen
  - PM2.5, PM10
  - SO2, NO2, CO
  - Ozon, Staub und AOD500 Konzentration
  - Sandsturmwarnung

#### 5. Multimodel Packages

##### [Multimodel](https://docs.meteoblue.com/en/weather-apis/packages-api/forecast-data#multimodel)

- **Funktion:** Rohmodelldaten für alle Modelle, die für den ausgewählten Standort verfügbar sind.
- **Verfügbare Intervalle:**
  - multimodel-1h (16000 Credits)
- **Wettervariablen:**
  - Temperatur
  - Windgeschwindigkeit und -richtung
  - Niederschlag
  - Wolkendecke
  - Temperatur und Windgeschwindigkeit mit Streuung
  - Kurzwellige Strahlung (GHI)

#### [Location Search API](https://docs.meteoblue.com/en/weather-apis/further-apis/location-search-api)

### ECMWF APIs

#### [ECMWF Web API](https://www.ecmwf.int/en/computing/software/ecmwf-web-api)

- **Funktion:** Bietet Zugriff auf Wetter- und Klimadaten, einschließlich Reanalysen, Vorhersagen und Atmosphärenanalysen.
- **Anwendungsbeispiel:** Abfrage von historischen Wetterdaten, Erstellen von Wettervorhersagen und Zugriff auf spezialisierte Klimadaten.

#### Verfügbare Datensätze:

##### ERA5

- **Beschreibung:** Der neueste Reanalyse-Datensatz mit stündlichen Daten und höherer räumlicher Auflösung.
- **Beispiele für Daten:** Temperatur, Niederschlag, Windgeschwindigkeit, Luftdruck, Strahlung, Bodenfeuchte.

##### ERA-Interim

- **Beschreibung:** Älterer Reanalyse-Datensatz, der seit 1979 verfügbar ist.
- **Beispiele für Daten:** Temperatur, Niederschlag, Windgeschwindigkeit, Luftdruck.

##### Seasonal Forecasts

- **Beschreibung:** Saisonale Vorhersagen für langfristige Wetter- und Klimaprognosen.
- **Beispiele für Daten:** Temperatur, Niederschlag, Anomalien.

##### Monthly Forecasts

- **Beschreibung:** Monatsvorhersagen für mittelfristige Wettertrends.
- **Beispiele für Daten:** Temperatur, Niederschlag, Wind.

##### Atmospheric Composition

- **Beschreibung:** Daten zur atmosphärischen Zusammensetzung, einschließlich Spurengase und Aerosole.
- **Beispiele für Daten:** Ozon, Kohlenmonoxid, Staub.

##### Copernicus Climate Change Service (C3S)

- **Beschreibung:** Daten und Dienstleistungen zur Klimaüberwachung und -prognose.
- **Beispiele für Daten:** Klimavariablen, Anomalien.

#### Beispiel für die Nutzung der ECMWF Web API:

```python
from ecmwfapi import ECMWFDataServer

server = ECMWFDataServer()

server.retrieve({
    'dataset': 'interim',
    'date': '2023-01-01/to/2023-01-31',
    'target': 'output.grib',
    'param': '167.128',  # Parameter ID für 2m Temperatur
    'step': '0',
    'levtype': 'sfc',
    'type': 'an',
    'class': 'ei',
    'grid': '0.75/0.75',
    'time': '00:00:00/06:00:00/12:00:00/18:00:00',
    'format': 'grib'
})
```
