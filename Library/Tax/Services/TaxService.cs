namespace Library.Tax.Calculator.Services
{
  public interface TaxService
  {
    int year {get;set;}

    EffectiveTaxRate Calculate(float income);
  }
}