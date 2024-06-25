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
        private readonly DBService _dbservice;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _weatherForecastService = new WeatherForecastService(configuration);
            _dbservice = new DBService(configuration);
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast(string city)
        {
            WeatherForecast finalResponse = new WeatherForecast();

            ForecastResponse? forecastResponse = _weatherForecastService.GetCurrentForecast(city);
            AstronomyResponse? astronomyResponse  = _weatherForecastService.GetAstronomy(city);
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
        
        [HttpGet("GetDBList")]
        public async Task<IActionResult> GetDBList(string countryName , string cityName) 
        { 
             List<WeatherForecast> response =  _dbservice.GetCityForecast(countryName , cityName);
             return Ok(response);
        }
    }
}
