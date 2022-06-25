using Library.Envelope;
using Library.Tax.Calculator;
using Microsoft.AspNetCore.Mvc;

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
            var taxRate = taxService.FetchTaxRate(income, year);
            if (taxRate != null) {
                return new Response<EffectiveTaxRate>(){
                    Result = taxRate,
                    Success = true
                };
            }
            throw new Exception("Not found tax for this query");
        }
        catch (Exception e) {
            return NotFound(new Response<EffectiveTaxRate>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
