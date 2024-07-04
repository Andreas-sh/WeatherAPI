using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            WeatherForecast finalResponse = new WeatherForecast();

            WeatherItem? DbItem = _context.WeatherItems
            .Include(i => i.Astronomy)
            .FirstOrDefault(i => i.CityName == city && i.CountryName == country);
            DateTime CurrentTime = DateTime.Now;

            if(DbItem != null && CurrentTime <= DbItem.UpdateTime.AddHours(1))
            {
                finalResponse.Name = DbItem.CityName;
                finalResponse.Temp = DbItem.temp;
                finalResponse.Time = DbItem.time;
                finalResponse.Weather = DbItem.forecast;
                finalResponse.weather_pic = DbItem.forecastIcon;
                finalResponse.Sunrise = DbItem.Astronomy.sunriseTime;
                finalResponse.sunset = DbItem.Astronomy.sunsetTime;
                return Ok(finalResponse);
            }
            
            ForecastResponse? forecastResponse = _weatherForecastService.GetCurrentForecast(city, country);
            AstronomyResponse? astronomyResponse  = _weatherForecastService.GetAstronomy(city, country);
            
            
            
            
           
            
            if(DbItem == null)
            {
                DbItem = new WeatherItem();

                DbItem.CityName = forecastResponse.Name;
                DbItem.CountryName = forecastResponse.Country;
                DbItem.forecast = forecastResponse.Weather;
                DbItem.temp = forecastResponse.Temp;
                DbItem.time = forecastResponse.Time;
                DbItem.forecastIcon = forecastResponse.weather_pic;
                DbItem.UpdateTime = DateTime.Now;

                DbItem.Astronomy = new AstronomyItem();
                DbItem.Astronomy.sunriseTime = astronomyResponse.Sunrise;
                DbItem.Astronomy.sunsetTime = astronomyResponse.sunset;
                
                _context.WeatherItems.Add(DbItem);
                await _context.SaveChangesAsync();
                
            }
            else if(CurrentTime > DbItem.UpdateTime.AddHours(1))
            {
                DbItem.forecast = forecastResponse.Weather;
                DbItem.temp = forecastResponse.Temp;
                DbItem.time = forecastResponse.Time;
                DbItem.forecastIcon = forecastResponse.weather_pic;
                DbItem.UpdateTime = DateTime.Now;
                
                DbItem.Astronomy.sunriseTime = astronomyResponse.Sunrise;
                DbItem.Astronomy.sunsetTime = astronomyResponse.sunset;
                
                _context.WeatherItems.Update(DbItem);
                await _context.SaveChangesAsync();
            }
            if (forecastResponse != null)
            {
                finalResponse.Name = DbItem.CityName;
                finalResponse.Temp = DbItem.temp;
                finalResponse.Time = DbItem.time;
                finalResponse.Weather = DbItem.forecast;
                finalResponse.weather_pic = DbItem.forecastIcon;
            }
            if(astronomyResponse != null)
            {
                finalResponse.Sunrise = DbItem.Astronomy.sunriseTime;
                finalResponse.sunset = DbItem.Astronomy.sunsetTime;
            }
            
            return Ok(finalResponse);

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
