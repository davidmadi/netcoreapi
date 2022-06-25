using Library.Tax.Calculator;

namespace SampleApi.Tests.Tax.Services;

public class MarginalTaxRateCalculatorTest {

  private List<Bracket> MockBrackets(){
    var brackets = new List<Bracket>();
    brackets.Add(new Bracket(){
      min = 0, 
      max = 20000, 
      rate = 0.0m
    });
    brackets.Add(new Bracket(){
      min = 20000, 
      max = 40000, 
      rate = 0.1m
    });
    brackets.Add(new Bracket(){
      min = 40000, 
      max = 60000, 
      rate = 0.2m
    });
    brackets.Add(new Bracket(){
      min = 60000, 
      max = 80000, 
      rate = 0.3m
    });
    return brackets;
  }


  [Fact]
  public void MarginalRate_50k_10k_example1()
  {
    decimal income = 50000;
    decimal raise = 10000;
    var marginalTaxResult = MarginalTaxRateCalculator.Calculate(income, raise, this.MockBrackets());

    Assert.NotNull(marginalTaxResult);
    Assert.Equal(marginalTaxResult.marginalTaxes, 2000m);
  }
  [Fact]
  public void MarginalRate_50k_15k_example2()
  {
    decimal income = 50000;
    decimal raise = 15000;
    var marginalTaxResult = MarginalTaxRateCalculator.Calculate(income, raise, this.MockBrackets());

    Assert.NotNull(marginalTaxResult);
    Assert.Equal(marginalTaxResult.marginalTaxes, 3500m);
  }

}