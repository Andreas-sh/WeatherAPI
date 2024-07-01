using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.models.weatherapi
{
    public class AstronomyItem
    {
        public long Id {get; set; }
        public string? sunriseTime { get; set; }
        public string? sunsetTime { get; set; }
    }
}