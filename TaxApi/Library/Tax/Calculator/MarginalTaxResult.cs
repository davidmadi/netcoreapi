namespace Library.Tax.Calculator
{
  public class MarginalTaxResult
  {
    internal decimal raiseTaxes;
    internal decimal maxThresholdPayableAmount;

    public decimal year{get;set;}
    public decimal income{get;set;}
    public decimal raise{get;set;}
    public decimal marginalTaxPayableAmount {get;set;}
    public decimal incomeTaxPayableAmount{get;set;}
  }
}