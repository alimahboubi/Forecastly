using Forecastly.Domain.Dto;
using Forecastly.Domain.Services;
using Forecastly.Domain.Weather;

namespace Forecastly.Application.Weather;

public class WeatherService(IWeatherRepository weatherRepository, ICacheService cacheService) : IWeatherService
{
    public async Task<Domain.Weather.Weather> GetCurrentWeatherAsync(
        CurrentWeatherRequestParameterDto requestDto,
        CancellationToken cancellationToken = default)
    {
        string cacheKey = GenerateCacheKey("current", requestDto.Lat, requestDto.Lon, requestDto.Units);

        var cachedValue = cacheService.Get<Domain.Weather.Weather?>(cacheKey);

        if (cachedValue != null) return cachedValue;
        cachedValue =
            await weatherRepository.GetCurrentWeatherAsync(requestDto, cancellationToken);
        cacheService.Set(cacheKey, cachedValue, TimeSpan.FromMinutes(15));

        return cachedValue;
    }

    public async Task<Domain.Weather.Weather> GetSpecificDayWeatherAsync(
        HistoricalWeatherRequestParameterDto requestDto,
        CancellationToken cancellationToken = default)
    {
        string cacheKey = GenerateCacheKey("historical", requestDto.Lat, requestDto.Lon, requestDto.Units,
            requestDto.Time);

        var cachedValue = cacheService.Get<Domain.Weather.Weather?>(cacheKey);

        if (cachedValue != null) return cachedValue;
        cachedValue = await weatherRepository.GetSpecificDayWeatherAsync(requestDto,
            cancellationToken);
        cacheService.Set(cacheKey, cachedValue, TimeSpan.FromHours(1));

        return cachedValue;
    }


    private string GenerateCacheKey(string prefix, double lat, double lon, string units, DateTime? time = null)
    {
        return time == null
            ? $"{prefix}-{lat}-{lon}-{units}"
            : $"{prefix}-{lat}-{lon}-{units}-{time}";
    }
}