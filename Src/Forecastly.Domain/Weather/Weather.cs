namespace Forecastly.Domain.Weather;

public class Weather
{
    public required string City { get; set; }
    public required DateTime Date { get; set; }
    public required string Condition { get; set; }
    public double AverageTemperature { get; set; }
}