using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.models.weatherapi
{
    public class WeatherItem
    {
        public long Id { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? temp { get; set; }
        public string? time { get; set; }
        public string? forecast { get; set; }
        public string? forecastIcon { get; set; }
        public string? UpdateTime {get; set; }
        
    }
}