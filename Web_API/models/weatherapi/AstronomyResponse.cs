namespace Web_API.models.weatherapi
{
    public class AstronomyResponse
    {
        public string? Sunrise { get; set; }
        public string? sunset { get; set; }
        public static DateTime Today { get; }
    }
}
