using Library.Tax.Calculator.Services;

namespace Library.Tax.Calculator;

public static class Factory {
  public static TaxService ByYear(int year) {

    if (year >= 2019 && year <= 2021)
      return new InterviewTestServer(year);

    throw new ArgumentOutOfRangeException();

  }


}