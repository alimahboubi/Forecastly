namespace Forecastly.Infrastructure.OpenWeatherMap.Models;

public class GeocodingResponse
{
    public string Name { get; set; }
    public Dictionary<string, string> LocalNames { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
}