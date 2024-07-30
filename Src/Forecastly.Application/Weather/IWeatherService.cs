using Forecastly.Domain.Dto;

namespace Forecastly.Application.Weather;

public interface IWeatherService
{
    Task<Domain.Weather.Weather> GetCurrentWeatherAsync(
        CurrentWeatherRequestParameterDto currentWeatherRequestParameterDto,
        CancellationToken cancellationToken = default);

    Task<Domain.Weather.Weather> GetSpecificDayWeatherAsync(
        HistoricalWeatherRequestParameterDto historicalWeatherRequestParameterDto,
        CancellationToken cancellationToken = default);
}