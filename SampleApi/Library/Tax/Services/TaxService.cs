namespace Library.Tax.Calculator.Services
{
  public interface TaxService
  {
    EffectiveTaxRate? Calculate(decimal income, int year);
    EffectiveTaxRate? Calculate(decimal income, int year, object response);
    public object? QueryOnline(int year);
  }

}

