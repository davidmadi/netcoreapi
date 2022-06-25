using Library.Tax.Calculator.Services;
using Library.Tax.Services.MemoryCache;

namespace Library.Tax.Calculator;

public static class Factory {

  private static Dictionary<int, Cacheable> hashYear = new Dictionary<int, Cacheable>();

  static Factory(){
    hashYear[2019] = new Cacheable(InterviewTestService.getInstance());
    hashYear[2020] = new Cacheable(InterviewTestService.getInstance());
    hashYear[2021] = new Cacheable(InterviewTestService.getInstance());
  }

  public static Cacheable ByYear(int year) {

    if (hashYear.ContainsKey(year))
      return hashYear[year];

    throw new ArgumentOutOfRangeException("Year " + year + " is not supported yet");
  }


}