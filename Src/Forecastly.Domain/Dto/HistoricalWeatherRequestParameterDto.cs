namespace Forecastly.Domain.Dto;

public record HistoricalWeatherRequestParameterDto(double Lat, double Lon, string? Units, DateTime Time);