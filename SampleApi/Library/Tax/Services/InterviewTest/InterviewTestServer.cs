
using System.Text.Json;
using Library.Tax.Calculator;
using Library.Tax.Calculator.Services;

public class InterviewTestServer : TaxService
{
  public int year {get;set;}

  public InterviewTestServer(int year){
    this.year = year;
  }

  private static readonly HttpClient client = new HttpClient();

  public EffectiveTaxRate Calculate(float income)
  {
    //var url = "http://interview-test-server:5000/tax-calculator/brackets/" + this.year;
    var url = "http://localhost:5001/tax-calculator/brackets/" + this.year;
    var stringTask = InterviewTestServer.client.GetStringAsync(url);
    stringTask.Wait();
    var response = JsonSerializer.Deserialize<IncomeResponse>(stringTask.Result);
    var sorted = response.tax_brackets.OrderBy((b) => b.min).ToList();

    foreach(var bracket in sorted) {
      if (bracket.min <= income && bracket.max >= income){
        return new EffectiveTaxRate(){
          Income = income,
          Year = year,
          Rate = 10
        };
      }
    }

    throw new ArgumentOutOfRangeException();
  }
}
