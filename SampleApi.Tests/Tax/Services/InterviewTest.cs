namespace SampleApi.Tests.Tax.Services;

public class InterviewTest {

  private IncomeResponse MockIncomeResponse(){
    var incomeResponse = new IncomeResponse();
    incomeResponse.tax_brackets = new List<Bracket>();
    incomeResponse.tax_brackets.Add(new Bracket(){
      max = 80000, 
      min = 0,
      rate = 0.8f
    });
    incomeResponse.tax_brackets.Add(new Bracket(){
      max = 120000, 
      min = 115000,
      rate = 0.9f
    });
    return incomeResponse;
  }


  [Fact]
  public void Calculate_Tax_2019_100k_null()
  {
    var rate = InterviewTestService.Calculate(100000, 2019, this.MockIncomeResponse());
    Assert.Equal(rate, null);
  }

  [Fact]
  public void Calculate_Tax_2019_110k_null()
  {
    var rate = InterviewTestService.Calculate(110000, 2019, this.MockIncomeResponse());
    Assert.Equal(rate, null);
  }

  [Fact]
  public void Calculate_Tax_2019_117k_found()
  {
    var rate = InterviewTestService.Calculate(117000, 2019, this.MockIncomeResponse());
    Assert.NotEqual(rate, null);
    Assert.Equal(rate.Rate, 0.9f);
  }

  [Fact]
  public void Calculate_Tax_2019_85k_null()
  {
    var rate = InterviewTestService.Calculate(85000, 2019, this.MockIncomeResponse());
    Assert.Equal(rate, null);
  }

  [Fact]
  public void Calculate_Tax_2019_75k_found()
  {
    var rate = InterviewTestService.Calculate(75000, 2019, this.MockIncomeResponse());
    Assert.NotEqual(rate, null);
    Assert.Equal(rate.Rate, 0.8f);
  }
}