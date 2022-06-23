using Microsoft.AspNetCore.Mvc;
using Weather;

namespace FirstApi.Controllers.v2;

[ApiController]
[Route("v2/api/weather")]

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
        var forecaster = ForecasterFactory.Build(WeatherVersion.v2);
        return forecaster.Get();
    }
    
    
    
}
