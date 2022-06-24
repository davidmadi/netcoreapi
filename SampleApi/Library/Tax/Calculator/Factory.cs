using Library.Tax.Calculator.Services;

namespace Library.Tax.Calculator;

public static class Factory {

  private static Dictionary<int, TaxService> hashYear = new Dictionary<int, TaxService>();

  static Factory(){
    hashYear[2019] = InterviewTestService.getInstance();
    hashYear[2020] = InterviewTestService.getInstance();
    hashYear[2021] = InterviewTestService.getInstance();
  }

  public static TaxService ByYear(int year) {

    if (hashYear.ContainsKey(year))
      return hashYear[year];

    throw new ArgumentOutOfRangeException();
  }


}