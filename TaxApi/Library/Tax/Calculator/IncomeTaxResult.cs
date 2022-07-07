namespace Library.Tax.Calculator
{
  public class IncomeTaxResult
  {
    public IncomeTaxResult(int year, decimal income){
      this.year = year;
      this.income = income;
    }

    public decimal year{get;set;}
    public decimal income{get;set;}
    public decimal incomeTaxPayableAmount{get;set;}
  }
}