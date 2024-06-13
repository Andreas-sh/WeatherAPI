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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _weatherForecastService = new WeatherForecastService(configuration);
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get(string city)
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

    }
}
