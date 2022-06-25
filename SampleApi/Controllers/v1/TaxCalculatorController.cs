using Library.Envelope;
using Library.Tax.Calculator;
using Library.Tax.Services;
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

    [HttpGet("marginalTaxCalculator/{year}/{income}/{raise}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<MarginalTaxResult>> Get(int year, decimal income, decimal raise)
    {
        try {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
                Request.Headers["Cache-Control"] != "no-cache";

            var taxService = Library.Tax.Calculator.Factory.ByYear(year);
            var brackets = taxService.FetchBrackets(year, withCache);
            var result = MarginalTaxRateCalculator.Calculate(income, raise, brackets);
            result.year = year;

            return new Response<MarginalTaxResult>(){
                Result = result,
                Success = true
            };
        }
        catch (Exception e) {
            return NotFound(new Response<MarginalTaxResult>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
