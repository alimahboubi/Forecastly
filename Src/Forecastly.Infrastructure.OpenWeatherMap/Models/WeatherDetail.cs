using System.Text.Json.Serialization;

namespace Forecastly.Infrastructure.OpenWeatherMap.Models;

public class WeatherDetail
{
    [JsonPropertyName("Dt")]
    public long CurrentTime { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public double Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }

    [JsonPropertyName("Weather")]
    public List<WeatherCondition>? Conditions { get; set; }
}