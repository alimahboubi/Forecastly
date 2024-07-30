using Forecastly.Application.Weather;
using Forecastly.Domain.Dto;
using Forecastly.Domain.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Forecastly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeathersController(IWeatherService weatherService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCurrent(
        [FromQuery] CurrentWeatherRequestParameterDto currentWeatherRequestParameterDto,
        CancellationToken cancellationToken)
    {
        var result = await weatherService.GetCurrentWeatherAsync(currentWeatherRequestParameterDto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> Getdaily(
        [FromQuery] HistoricalWeatherRequestParameterDto historicalWeatherRequestParameterDto,
        CancellationToken cancellationToken)
    {
        var result =
            await weatherService.GetSpecificDayWeatherAsync(historicalWeatherRequestParameterDto, cancellationToken);
        return Ok(result);
    }
}