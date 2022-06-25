
using System.Text.Json;
using Library.Tax.Calculator;
using Library.Tax.Calculator.Services;

public class InterviewTestService : TaxService
{
  private static InterviewTestService? singleton;
  public static TaxService getInstance(){
    if (InterviewTestService.singleton == null){
      InterviewTestService.singleton = new InterviewTestService();
    }
    return InterviewTestService.singleton;
  }

  public object? QueryOnline(int year) {
    //var url = "http://interview-test-server:5000/tax-calculator/brackets/" + this.year;
    var url = "http://localhost:5001/tax-calculator/brackets/" + year;
    HttpClient client = new HttpClient();
    var stringTask = client.GetStringAsync(url);
    stringTask.Wait();
    IncomeResponse? response = JsonSerializer.Deserialize<IncomeResponse>(stringTask.Result);
    if(response?.tax_brackets?.Count() > 0){
      return response;
    }
    return null;
  }

  public EffectiveTaxRate? Calculate(float income, int year)
  {
    object? response = this.QueryOnline(year);
    if (response != null){
      return this.Calculate(income, year, response);
    }
    throw new Exception("No result found for year: " + year);
  }

  public EffectiveTaxRate? Calculate(float income, int year, object response){
    var sorted = ((IncomeResponse)response).tax_brackets?.OrderBy((b) => b.min).ToList();

    if (sorted != null){
      foreach(var bracket in sorted) {
        if (bracket.min <= income && income <= bracket.max){
          return new EffectiveTaxRate(){
            Income = income,
            Year = year,
            Rate = bracket.rate
          };
        }
      }
    }
    return null;
  }

}
