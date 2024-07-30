namespace Forecastly.Domain.Dto;

public record CurrentWeatherRequestParameterDto(double Lat, double Lon, string? Units);