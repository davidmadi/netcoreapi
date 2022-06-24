using Library.Envelope;
using Library.Tax.Calculator;
using Microsoft.AspNetCore.Mvc;
using Weather;

namespace SampleApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class TaxCalculatorController : ControllerBase
{

    private readonly ILogger<TaxCalculatorController> _logger;

    public TaxCalculatorController(ILogger<TaxCalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("calculator/{year}/{income}")]   
    public Response<EffectiveTaxRate> Get(int year, float income)
    {
        return new Response<EffectiveTaxRate>(){
            Result = new EffectiveTaxRate(){
                Income = income,
                Year = year,
                Rate = 10
            },
            Success = true,
            ReliabilityEnum = Reliability.Online
        };
    }    
    
}
