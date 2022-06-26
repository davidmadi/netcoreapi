
using System.Text.Json;
using System.Text.Json.Serialization;
using Library.Tax.Calculator.Services;

public class InterviewTestService : TaxService
{
  protected int year { get ; set; }

  public InterviewTestService(int year) {
    this.year = year;
  }

  public QueryBracketResponse? QueryOnline() {

    //TODO: implement environment variable for dev url and live url
    //var url = "http://interview-test-server:5000/tax-calculator/brackets/" + this.year;
    var url = "http://localhost:5001/tax-calculator/brackets/" + this.year;

    var response = HttpProxy.HttpJsonCall<QueryBracketResponse>(null, url, "InterviewTestServer");
    if(response?.tax_brackets?.Count() > 0){
      return response;
    }
    return null;
  }
}
