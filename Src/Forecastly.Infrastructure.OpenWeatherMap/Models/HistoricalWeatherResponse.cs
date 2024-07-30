namespace Forecastly.Infrastructure.OpenWeatherMap.Models;

public class HistoricalWeatherResponse
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    public int TimezoneOffset { get; set; }
    public List<WeatherDetail> Data { get; set; }
}