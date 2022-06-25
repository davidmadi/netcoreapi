namespace SampleApi.Tests.Tax.Services;

public class InterviewTest {

  private IncomeResponse MockIncomeResponse(){
    var incomeResponse = new IncomeResponse();
    incomeResponse.tax_brackets = new List<Bracket>();
    incomeResponse.tax_brackets.Add(new Bracket(){
      max = 80000, 
      rate = 0.8m
    });
    incomeResponse.tax_brackets.Add(new Bracket(){
      max = 120000, 
      min = 115000,
      rate = 0.9m
    });
    incomeResponse.tax_brackets.Add(new Bracket(){
      min = 125000,
      rate = 0.9m
    });
    return incomeResponse;
  }


  [Fact]
  public void Calculate_Tax_2019_100k_null()
  {
    var rate = new InterviewTestService().Calculate(100000, 2019, this.MockIncomeResponse());
    Assert.Null(rate);
  }

  [Fact]
  public void Calculate_Tax_2019_110k_null()
  {
    var rate = new InterviewTestService().Calculate(110000, 2019, this.MockIncomeResponse());
    Assert.Null(rate);
  }

  [Fact]
  public void Calculate_Tax_2019_117k_found()
  {
    var rate = new InterviewTestService().Calculate(117000, 2019, this.MockIncomeResponse());
    Assert.NotNull(rate);
    Assert.Equal(0.9m, rate?.Rate);
  }

  [Fact]
  public void Calculate_Tax_2019_85k_null()
  {
    var rate = new InterviewTestService().Calculate(85000, 2019, this.MockIncomeResponse());
    Assert.Null(rate);
  }

  [Fact]
  public void Calculate_Tax_2019_75k_found()
  {
    var rate = new InterviewTestService().Calculate(75000, 2019, this.MockIncomeResponse());
    Assert.NotNull(rate);
    Assert.Equal(0.8m, rate?.Rate);
  }

  [Fact]
  public void Calculate_Tax_2019_130k_found()
  {
    var rate = new InterviewTestService().Calculate(130000, 2019, this.MockIncomeResponse());
    Assert.NotNull(rate);
    Assert.Equal(0.9m, rate?.Rate);
  }
}