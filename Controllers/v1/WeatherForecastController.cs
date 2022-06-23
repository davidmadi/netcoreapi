using Microsoft.AspNetCore.Mvc;
using Weather;

namespace FirstApi.Controllers;

[ApiController]
[Route("api/weather")]

public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("v1/forecast")]   
    public IEnumerable<WeatherForecast> GetV1()
    {
        var forecaster = ForecasterFactory.Build(WeatherVersion.v1);
        return forecaster.Get();
    }
    
    [HttpGet("v2/forecast")]   
    public IEnumerable<WeatherForecast> GetV2()
    {
        var forecaster = ForecasterFactory.Build(WeatherVersion.v2);
        return forecaster.Get();
    }
    
    
    
}
