namespace Library.Tax.Calculator.Services
{
  public interface TaxService
  {
    EffectiveTaxRate Calculate(float income, int year);
  }
}