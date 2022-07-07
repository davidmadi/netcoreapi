using Library.Tax.Calculator;

namespace TaxApi.Tests.Tax.Services;

public class IncomeTaxCalculatorTest {

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


  private List<Bracket> NoMaxMockBrackets(){
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
    brackets.Add(new Bracket(){
      min = 80000, 
      rate = 0.3m
    });
    return brackets;
  }


  [Fact]
  public void MarginalRate_50k_10k_example1()
  {
    decimal income = 50000;
    decimal raise = 10000;
    var incomeTaxResult = IncomeTaxCalculator.Calculate(2019, income, raise, this.MockBrackets());

    Assert.NotNull(incomeTaxResult);
    Assert.Equal(incomeTaxResult.marginalTaxPayableAmount, 2000m);
  }

  [Fact]
  public void MarginalRate_50k_15k_example2()
  {
    decimal income = 50000;
    decimal raise = 15000;
    var incomeTaxResult = IncomeTaxCalculator.Calculate(2019, income, raise, this.MockBrackets());

    Assert.NotNull(incomeTaxResult);
    Assert.Equal(incomeTaxResult.marginalTaxPayableAmount, 3500m);
  }

  [Fact]
  public void MarginalRate_80k_10k_80Max()
  {
    decimal income = 80000;
    decimal raise = 10000;
    var incomeTaxResult = IncomeTaxCalculator.Calculate(2019, income, raise, this.MockBrackets());

    Assert.NotNull(incomeTaxResult);
    Assert.Equal(incomeTaxResult.marginalTaxPayableAmount, 0);
  }


  [Fact]
  public void MarginalRate_80k_10k_NoMax()
  {
    decimal income = 80000;
    decimal raise = 10000;
    var incomeTaxResult = IncomeTaxCalculator.Calculate(2019, income, raise, this.NoMaxMockBrackets());

    Assert.NotNull(incomeTaxResult);
    Assert.Equal(incomeTaxResult.marginalTaxPayableAmount, 3000);
  }

}