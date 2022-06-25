namespace Library.Tax.Services.MemoryCache;

using Library.Tax.Calculator;
using Library.Tax.Calculator.Services;

public class Cacheable {
  TaxService service;

  Dictionary<int, object> yearCache = new Dictionary<int, object>();
  //Dependency injection for cache
  public Cacheable(TaxService? taxService){
    if(taxService == null)
      throw new ArgumentNullException();

    this.service = taxService;
  }

  public EffectiveTaxRate FetchTaxRate(decimal income, int year, bool withCache)
  {
    if (yearCache.ContainsKey(year) && withCache){
      var cachedResult = yearCache[year];
      var rate = this.service.Calculate(income, year, cachedResult);
      if(rate != null){
        rate.ReliabilityEnum = Reliability.Cache;
        return rate;
      }
    } else {
      var onlineResult = this.service.QueryOnline(year);
      if (onlineResult != null) {
        yearCache[year] = onlineResult;
        var rate = this.service.Calculate(income, year, onlineResult);
        if(rate != null){
          rate.ReliabilityEnum = Reliability.Online;
          return rate;
        }
      }
    }
    throw new Exception("Not found tax for this query");
  }
}