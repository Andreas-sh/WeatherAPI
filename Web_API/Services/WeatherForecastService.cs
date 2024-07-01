using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Web_API.models;
using Web_API.models.weatherapi;

namespace Web_API.Services;

public class WeatherForecastService
{
    private readonly IConfiguration _configuration;
    private readonly string WeatherApiKey;

    public WeatherForecastService(IConfiguration configuration)
    {
        _configuration = configuration;
        WeatherApiKey = _configuration["MyCustomKey"];
    }
    
    public ForecastResponse? GetCurrentForecast(string forecastCity, string forecastCountry)
    {
        using (HttpClient client = GetBaseHttpClient())
        {
            string url = "forecast.json?key=" + WeatherApiKey + "&q=" + forecastCity + " " + forecastCountry + "&days=1&aqi=no&alerts=no";
            HttpResponseMessage response =  client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var jsonObject = JObject.Parse(result);

                ForecastResponse forecastResponse = new ForecastResponse();
                forecastResponse.Name = jsonObject["location"]?["name"]?.Value<string>();
                forecastResponse.Country = jsonObject["location"]?["country"]?.Value<string>();
                forecastResponse.Temp = jsonObject["current"]?["temp_c"]?.Value<string>();
                forecastResponse.Time = jsonObject["location"]?["localtime"]?.Value<string>();
                forecastResponse.Weather = jsonObject["current"]?["condition"]?["text"]?.Value<string>();
                forecastResponse.weather_pic = jsonObject["current"]?["condition"]?["icon"]?.Value<string>();

                return forecastResponse;

            }
            else
            {
                return null;
            }
        }

    }

    public AstronomyResponse? GetAstronomy(string forecastCity, string forecastCountry)
    {
        using (HttpClient client = GetBaseHttpClient())
        {
            DateTime thisDay = DateTime.Today;
            string url2 = "astronomy.json?key=" + WeatherApiKey + "&q=" + forecastCity  + " " + forecastCountry + "&dt=" + thisDay.ToString("yyyy-MM-dd");
            HttpResponseMessage response2 = client.GetAsync(url2).Result;
            if (response2.IsSuccessStatusCode)
            {
                string result2 = response2.Content.ReadAsStringAsync().Result;
                var jsonObject2 = JObject.Parse(result2);
                AstronomyResponse astronomyResponse = new AstronomyResponse();
                astronomyResponse.Sunrise = jsonObject2["astronomy"]?["astro"]?["sunrise"]?.Value<string>();
                astronomyResponse.sunset = jsonObject2["astronomy"]?["astro"]?["sunset"]?.Value<string>();

                return astronomyResponse;
            }
            else
            {
                return null;
            }
        }
    }
    public AutocompleteResponse[]? SearchCities(string forecastCity)
    {
        using (HttpClient client = GetBaseHttpClient())
        {
            string url3 = "search.json?key=" + WeatherApiKey + "&q=" + forecastCity;
            HttpResponseMessage response3 = client.GetAsync(url3).Result;
            if(response3.IsSuccessStatusCode){
                string result3 = response3.Content.ReadAsStringAsync().Result;
                var jsonObject3 = JsonObject.Parse(result3);
                JsonArray arr = jsonObject3.AsArray();
                AutocompleteResponse[] responseArr = new AutocompleteResponse[arr.Count]; 
                for(int i=0; i<arr.Count; i++){
                    AutocompleteResponse autocompleteResponse = new AutocompleteResponse();
                    autocompleteResponse.Name = arr[i]["name"].GetValue<string>();
                    autocompleteResponse.Country = arr[i]["country"].GetValue<string>();
                    responseArr[i] = autocompleteResponse;
                }

                
                
                return responseArr;
            }
            else{
                return null;
            }
        }

    }

    private HttpClient GetBaseHttpClient()
    {
        HttpClient client = new HttpClient();

        string? baseUrl = _configuration["API_url"];
        if (baseUrl != null)
            client.BaseAddress = new Uri(baseUrl);

        return client;
    }
}
