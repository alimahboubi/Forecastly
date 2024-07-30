using Forecastly.Application.Weather;
using Forecastly.Domain.Dto;
using Forecastly.Domain.Exceptions;
using Forecastly.Domain.Services;
using Forecastly.Domain.Weather;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Forecastly.Application.UnitTests;

public class WeatherServiceTests
{
    private readonly WeatherService _weatherService;
    private readonly Mock<IWeatherRepository> _weatherRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;

    public WeatherServiceTests()
    {
        _weatherRepositoryMock = new Mock<IWeatherRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _weatherService = new WeatherService(_weatherRepositoryMock.Object, _cacheServiceMock.Object);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_ShouldReturnWeather_FromCache()
    {
        // Arrange
        var requestDto = new CurrentWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric");
        var cachedWeather = new Domain.Weather.Weather
            { City = "Test City", Date = DateTime.Now, Condition = "Sunny", AverageTemperature = 25.0 };
        var cacheKey = $"current-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns(cachedWeather);

        // Act
        var result = await _weatherService.GetCurrentWeatherAsync(requestDto);

        // Assert
        Assert.Equal(cachedWeather, result);
        _weatherRepositoryMock.Verify(
            x => x.GetCurrentWeatherAsync(It.IsAny<CurrentWeatherRequestParameterDto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_ShouldReturnWeather_FromRepository_WhenCacheIsEmpty()
    {
        // Arrange
        var requestDto = new CurrentWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric");
        var fetchedWeather = new Domain.Weather.Weather
            { City = "Test City", Date = DateTime.Now, Condition = "Sunny", AverageTemperature = 25.0 };
        var cacheKey = $"current-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns((Domain.Weather.Weather?)null);
        _weatherRepositoryMock.Setup(x => x.GetCurrentWeatherAsync(requestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fetchedWeather);

        // Act
        var result = await _weatherService.GetCurrentWeatherAsync(requestDto);

        // Assert
        Assert.Equal(fetchedWeather, result);
        _cacheServiceMock.Verify(x => x.Set(cacheKey, fetchedWeather, TimeSpan.FromMinutes(15)), Times.Once);
    }

    [Fact]
    public async Task
        GetCurrentWeatherAsync_ShouldThrowWeatherNotFoundException_WhenRepositoryThrowsWeatherDataNotFoundException()
    {
        // Arrange
        var requestDto = new CurrentWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric");
        var cacheKey = $"current-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns((Domain.Weather.Weather?)null);
        _weatherRepositoryMock.Setup(x => x.GetCurrentWeatherAsync(requestDto, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new WeatherDataNotFoundException("Current weather data not found."));

        // Act & Assert
        await Assert.ThrowsAsync<WeatherDataNotFoundException>(() =>
            _weatherService.GetCurrentWeatherAsync(requestDto));
    }

    [Fact]
    public async Task GetSpecificDayWeatherAsync_ShouldReturnWeather_FromCache()
    {
        // Arrange
        var requestDto =
            new HistoricalWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric", Time: DateTime.Now);
        var cachedWeather = new Domain.Weather.Weather
            { City = "Test City", Date = DateTime.Now, Condition = "Sunny", AverageTemperature = 25.0 };
        var cacheKey = $"historical-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}-{requestDto.Time}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns(cachedWeather);

        // Act
        var result = await _weatherService.GetSpecificDayWeatherAsync(requestDto);

        // Assert
        Assert.Equal(cachedWeather, result);
        _weatherRepositoryMock.Verify(
            x => x.GetSpecificDayWeatherAsync(It.IsAny<HistoricalWeatherRequestParameterDto>(),
                It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetSpecificDayWeatherAsync_ShouldReturnWeather_FromRepository_WhenCacheIsEmpty()
    {
        // Arrange
        var requestDto =
            new HistoricalWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric", Time: DateTime.Now);
        var fetchedWeather = new Domain.Weather.Weather
            { City = "Test City", Date = DateTime.Now, Condition = "Sunny", AverageTemperature = 25.0 };
        var cacheKey = $"historical-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}-{requestDto.Time}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns((Domain.Weather.Weather?)null);
        _weatherRepositoryMock.Setup(x => x.GetSpecificDayWeatherAsync(requestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fetchedWeather);

        // Act
        var result = await _weatherService.GetSpecificDayWeatherAsync(requestDto);

        // Assert
        Assert.Equal(fetchedWeather, result);
        _cacheServiceMock.Verify(x => x.Set(cacheKey, fetchedWeather, TimeSpan.FromHours(1)), Times.Once);
    }

    [Fact]
    public async Task
        GetSpecificDayWeatherAsync_ShouldThrowWeatherNotFoundException_WhenRepositoryThrowsWeatherDataNotFoundException()
    {
        // Arrange
        var requestDto =
            new HistoricalWeatherRequestParameterDto(Lat: 10, Lon: 20, Units: "metric", Time: DateTime.Now);
        var cacheKey = $"historical-{requestDto.Lat}-{requestDto.Lon}-{requestDto.Units}-{requestDto.Time}";

        _cacheServiceMock.Setup(x => x.Get<Domain.Weather.Weather?>(cacheKey)).Returns((Domain.Weather.Weather?)null);
        _weatherRepositoryMock.Setup(x => x.GetSpecificDayWeatherAsync(requestDto, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new WeatherDataNotFoundException("Historical weather data not found."));

        // Act & Assert
        await Assert.ThrowsAsync<WeatherDataNotFoundException>(() =>
            _weatherService.GetSpecificDayWeatherAsync(requestDto));
    }
}