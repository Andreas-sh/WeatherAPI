using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Web_API.models;
using Web_API.models.weatherapi;
using Web_API.Services;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;
        private readonly DBcontext _context;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration,  DBcontext context)
        {
            _logger = logger;
            _weatherForecastService = new WeatherForecastService(configuration);
            _context = context;
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast(string city, string country)
        {
            WeatherItem? DbItem = _context.WeatherItems.FirstOrDefault(i => i.CityName == city && i.CountryName == country);
            if(DbItem == null)
            {
                
            }
            WeatherForecast finalResponse = new WeatherForecast();

            ForecastResponse? forecastResponse = _weatherForecastService.GetCurrentForecast(city, country);
            AstronomyResponse? astronomyResponse  = _weatherForecastService.GetAstronomy(city, country);
            if (forecastResponse != null)
            {
                finalResponse.Name = forecastResponse.Name;
                finalResponse.Temp = forecastResponse.Temp;
                finalResponse.Time = forecastResponse.Time;
                finalResponse.Weather = forecastResponse.Weather;
                finalResponse.weather_pic = forecastResponse.weather_pic;
            }
            if(astronomyResponse != null)
            {
                finalResponse.Sunrise = astronomyResponse.Sunrise;
                finalResponse.sunset = astronomyResponse.sunset;
            }
            
            return Ok(finalResponse);
            //return BadRequest();

        }

        [HttpGet("GetCityNames")]
        public async Task<IActionResult> GetCityNames(string? city)
        {
            if (string.IsNullOrWhiteSpace(city)){
                return BadRequest("City was not be empty");
            }
            if (city.Length < 3)
            {
                return BadRequest("City name should be more than 2 characters");
            }
            
            AutocompleteResponse[]? autoCompleteResponse = _weatherForecastService.SearchCities(city);
            return Ok(autoCompleteResponse);
        }
    }
}
