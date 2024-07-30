using System.Text.Json.Serialization;

namespace Forecastly.Infrastructure.OpenWeatherMap.Models;

public class WeatherResponse
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    public int TimezoneOffset { get; set; }
    public WeatherDetail Current { get; set; }
}