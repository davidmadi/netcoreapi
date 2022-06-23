using Microsoft.AspNetCore.Mvc;
using Weather;

namespace SampleApi.Controllers.v1;

[ApiController]
[Route("v1/api/weather")]

public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("forecast")]   
    public IEnumerable<WeatherForecast> Get()
    {
        var forecaster = ForecasterFactory.Build(WeatherVersion.v1);
        return forecaster.Get();
    }    
    
}
