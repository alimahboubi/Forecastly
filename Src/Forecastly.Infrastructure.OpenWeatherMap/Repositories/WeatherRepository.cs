using System.Net.Http.Json;
using Forecastly.Domain.Dto;
using Forecastly.Domain.Exceptions;
using Forecastly.Domain.Extensions;
using Forecastly.Domain.Weather;
using Forecastly.Infrastructure.OpenWeatherMap.Models;

namespace Forecastly.Infrastructure.OpenWeatherMap.Repositories
{
    public class WeatherRepository(HttpClient httpClient, OpenWeatherMapConfiguration configuration)
        : IWeatherRepository
    {
        public async Task<Weather> GetCurrentWeatherAsync(CurrentWeatherRequestParameterDto requestDto,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(requestDto);

            string query = BuildCurrentWeatherQuery(requestDto);
            var weatherDataResponse = await httpClient.GetFromJsonAsync<WeatherResponse>(query, cancellationToken);

            return ConvertCurrentWeatherResponseToWeather(weatherDataResponse)
                   ?? throw new WeatherDataNotFoundException("Current weather data not found.");
        }

        public async Task<Weather> GetSpecificDayWeatherAsync(HistoricalWeatherRequestParameterDto requestDto,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(requestDto);

            httpClient.BaseAddress = new Uri(httpClient.BaseAddress, "onecall/timemachine");
            string query = BuildHistoricalWeatherQuery(requestDto);
            var weatherDataResponse =
                await httpClient.GetFromJsonAsync<HistoricalWeatherResponse>(query, cancellationToken);

            return ConvertWeatherResponseToWeather(weatherDataResponse)
                   ?? throw new WeatherDataNotFoundException("Historical weather data not found.");
        }

        private void ValidateRequest(object requestDto)
        {
            if (requestDto == null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }
        }

        private string BuildCurrentWeatherQuery(CurrentWeatherRequestParameterDto requestDto)
        {
            return $"?lat={requestDto.Lat}&lon={requestDto.Lon}&units={requestDto.Units}&appid={configuration.ApiKey}";
        }

        private string BuildHistoricalWeatherQuery(HistoricalWeatherRequestParameterDto requestDto)
        {
            return $"?lat={requestDto.Lat}&lon={requestDto.Lon}&dt={requestDto.Time.ToUnixTimeSeconds()}" +
                   $"&units={requestDto.Units}&appid={configuration.ApiKey}";
        }

        private Weather? ConvertCurrentWeatherResponseToWeather(WeatherResponse? weatherResponse)
        {
            if (weatherResponse == null || weatherResponse.Current == null)
            {
                return null;
            }

            return new Weather
            {
                City = weatherResponse.Timezone,
                Date = weatherResponse.Current.CurrentTime.FromUnixTimeSeconds(),
                AverageTemperature = weatherResponse.Current.Temp,
                Condition = weatherResponse.Current.Conditions?.FirstOrDefault()?.Description ?? ""
            };
        }

        private Weather? ConvertWeatherResponseToWeather(HistoricalWeatherResponse? weatherResponse)
        {
            if (weatherResponse == null || !weatherResponse.Data.Any())
            {
                return null;
            }

            var weatherData = weatherResponse.Data.First();
            return new Weather
            {
                City = weatherResponse.Timezone,
                Date = weatherData.CurrentTime.FromUnixTimeSeconds(),
                AverageTemperature = weatherData.Temp,
                Condition = weatherData.Conditions?.FirstOrDefault()?.Description ?? ""
            };
        }
    }
}