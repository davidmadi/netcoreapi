using Library.Envelope;
using Library.Tax.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class TaxCalculatorController : ControllerBase
{
    [HttpGet("marginalTaxCalculator/{year}/{income}/{raise}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<MarginalTaxResult>> Get(int year, decimal income, decimal raise)
    {
        try {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
                Request.Headers["Cache-Control"] != "no-cache";

            var taxService = Library.Tax.Calculator.Factory.GetTaxServiceBy(year);
            var brackets = taxService.FetchBrackets(year, withCache);
            var marginalTaxResult = MarginalTaxRateCalculator.Calculate(income, raise, brackets);
            marginalTaxResult.year = year;

            return new Response<MarginalTaxResult>(){
                Result = marginalTaxResult,
                Success = true
            };
        }
        catch (Exception e) {
            Library.Logging.LogManager.EnqueueException(e);
            return NotFound(new Response<MarginalTaxResult>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
