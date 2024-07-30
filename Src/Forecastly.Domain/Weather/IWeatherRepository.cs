using Forecastly.Domain.Dto;

namespace Forecastly.Domain.Weather;

public interface IWeatherRepository
{
    Task<Weather> GetCurrentWeatherAsync(CurrentWeatherRequestParameterDto currentWeatherRequestParameterDto,
        CancellationToken cancellationToken = default);

    Task<Weather> GetSpecificDayWeatherAsync(HistoricalWeatherRequestParameterDto historicalWeatherRequestParameterDto,
        CancellationToken cancellationToken = default);
}