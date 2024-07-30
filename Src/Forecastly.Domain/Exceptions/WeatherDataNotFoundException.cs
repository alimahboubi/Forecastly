namespace Forecastly.Domain.Exceptions;

public class WeatherDataNotFoundException : Exception
{
    public WeatherDataNotFoundException(string message) : base(message) { }
}