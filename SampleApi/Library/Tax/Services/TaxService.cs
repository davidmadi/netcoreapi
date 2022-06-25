namespace Library.Tax.Calculator.Services
{
  public interface TaxService
  {
    EffectiveTaxRate? Calculate(float income, int year);
    EffectiveTaxRate? Calculate(float income, int year, object response);
    public object? QueryOnline(int year);
  }

}

