namespace Library.Tax.Calculator
{
  public class MarginalTaxResult
  {
    internal decimal incomeTaxes;
    internal decimal raiseTaxes;
    internal decimal windowToMaxTaxes;

    public decimal year{get;set;}
    public decimal income{get;set;}
    public decimal raise{get;set;}
    public decimal marginalTaxes {get;set;}
  }
}