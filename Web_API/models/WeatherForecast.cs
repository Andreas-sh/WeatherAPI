using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.models;

public class WeatherForecast
{
    public string? Name { get; set; }
    public string? Country { get; set;}
    public string? Temp { get; set; }
    public string? Time { get; set; }
    public string? Weather { get; set; }
    public string? weather_pic { get; set; }
    public string? Sunrise { get; set; }
    public string? sunset { get; set; }
    public static DateTime Today { get; }
}