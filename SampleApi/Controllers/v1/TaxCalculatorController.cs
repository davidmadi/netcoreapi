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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<EffectiveTaxRate>> Get(int year, float income)
    {
        try {
            var taxService = Library.Tax.Calculator.Factory.ByYear(year);
            var taxRate = taxService.Calculate(income, year);
            if (taxRate != null) {
                return new Response<EffectiveTaxRate>(){
                    Result = taxRate,
                    Success = true,
                    ReliabilityEnum = Reliability.Online
                };
            }
            throw new Exception();
        }
        catch (Exception) {
            return NotFound(new Response<EffectiveTaxRate>(){
                Success = false,
                ReliabilityEnum = Reliability.Error
            });
        }
    }    
    
}
