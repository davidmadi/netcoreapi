
using System.Text.Json;
using System.Text.Json.Serialization;
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

  public QueryBracketResponse? QueryOnline(int year) {
    //var url = "http://interview-test-server:5000/tax-calculator/brackets/" + this.year;
    var url = "http://localhost:5001/tax-calculator/brackets/" + year;
    HttpClient client = new HttpClient();
    var stringTask = client.GetStringAsync(url);
    stringTask.Wait();


    var options = new JsonSerializerOptions()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString |
        JsonNumberHandling.WriteAsString
    };

    QueryBracketResponse? response = JsonSerializer.Deserialize<QueryBracketResponse>(stringTask.Result, options);
    if(response?.tax_brackets?.Count() > 0){
      return response;
    }
    return null;
  }

}
