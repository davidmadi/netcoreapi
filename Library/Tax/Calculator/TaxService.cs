namespace Library.Tax.Calculator
{
  public interface TaxService
  {
    EffectiveTaxRate Calculate(float income);
  }
}