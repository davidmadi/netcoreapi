
using System.Text.Json;
using Library.Tax.Calculator;
using Library.Tax.Calculator.Services;

public class InterviewTestService : TaxService
{
  public static TaxService getInstance(){
    return new InterviewTestService();
  }

  public EffectiveTaxRate Calculate(float income, int year)
  {
    //var url = "http://interview-test-server:5000/tax-calculator/brackets/" + this.year;
    var url = "http://localhost:5001/tax-calculator/brackets/" + year;
    HttpClient client = new HttpClient();
    var stringTask = client.GetStringAsync(url);
    stringTask.Wait();
    var response = JsonSerializer.Deserialize<IncomeResponse>(stringTask.Result);
    return InterviewTestService.Calculate(income, year, response);
  }

  public static EffectiveTaxRate Calculate(float income, int year, IncomeResponse response){
    var sorted = response.tax_brackets.OrderBy((b) => b.min).ToList();

    foreach(var bracket in sorted) {
      if (bracket.min <= income && income <= bracket.max){
        return new EffectiveTaxRate(){
          Income = income,
          Year = year,
          Rate = bracket.rate
        };
      }
    }
    return null;
  }

  EffectiveTaxRate TaxService.Calculate(float income, int year)
  {
    throw new NotImplementedException();
  }

}
