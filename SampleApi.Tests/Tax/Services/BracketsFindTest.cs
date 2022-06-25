using Library.Tax.Calculator;

namespace SampleApi.Tests.Tax.Services;

public class BracketsFindTest {

  private List<Bracket> MockBrackets(){
    var brackets = new List<Bracket>();
    brackets.Add(new Bracket(){
      max = 80000, 
      rate = 0.8m
    });
    brackets.Add(new Bracket(){
      max = 120000, 
      min = 115000,
      rate = 0.9m
    });
    brackets.Add(new Bracket(){
      min = 125000,
      rate = 0.9m
    });
    return brackets;
  }


  [Fact]
  public void FindTaxRate_Tax_100k_null()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(100000, this.MockBrackets());
    Assert.Null(rate);
  }

  [Fact]
  public void FindTaxRate_Tax_110k_null()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(110000, this.MockBrackets());
    Assert.Null(rate);
  }

  [Fact]
  public void FindTaxRate_Tax_117k_found()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(117000, this.MockBrackets());
    Assert.NotNull(rate);
    Assert.Equal(0.9m, rate?.rate);
  }

  [Fact]
  public void FindTaxRate_Tax_85k_null()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(85000, this.MockBrackets());
    Assert.Null(rate);
  }

  [Fact]
  public void FindTaxRate_Tax_75k_found()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(75000, this.MockBrackets());
    Assert.NotNull(rate);
    Assert.Equal(0.8m, rate?.rate);
  }

  [Fact]
  public void FindTaxRate_Tax_130k_found()
  {
    var rate = MarginalTaxRateCalculator.FindInBracketBy(130000, this.MockBrackets());
    Assert.NotNull(rate);
    Assert.Equal(0.9m, rate?.rate);
  }
}