using System;
using Library.Envelope;
using Library.Tax.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaxApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class TaxCalculatorController : ControllerBase
{
    [HttpGet("calculator/{year}/{income}/{raise}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<IncomeTaxResult>> Get(int year, decimal income, decimal raise)
    {
        try {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
                Request.Headers["Cache-Control"] != "no-cache";

            var taxService = Library.Tax.Calculator.Factory.GetTaxServiceBy(year);
            var brackets = taxService.FetchBrackets(year, withCache);
            var incomeTaxResult = MarginalTaxRateCalculator.Calculate(income, raise, brackets);
            incomeTaxResult.year = year;

            return new Response<IncomeTaxResult>(){
                Result = incomeTaxResult,
                Success = true
            };
        }
        catch (Exception e) {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
