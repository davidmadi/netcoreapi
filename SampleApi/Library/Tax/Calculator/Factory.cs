using Library.Tax.Calculator.Services;
using Library.Tax.MemoryCache;

namespace Library.Tax.Calculator;

public static class Factory {

  private static Dictionary<int, Cacheable> hashYear = new Dictionary<int, Cacheable>();

  static Factory(){
    hashYear[2019] = new Cacheable(new InterviewTestService(2019));
    hashYear[2020] = new Cacheable(new InterviewTestService(2020));
    hashYear[2021] = new Cacheable(new InterviewTestService(2021));
  }

  public static Cacheable GetTaxServiceBy(int year) {

    if (hashYear.ContainsKey(year))
      return hashYear[year];

    throw new ArgumentOutOfRangeException("Year " + year + " is not supported yet");
  }


}