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
      object? response = yearCache[year];
      var rate = this.service.Calculate(income, year, response);
      if(rate != null){
        rate.ReliabilityEnum = Reliability.Cache;
        return rate;
      }
    } else {
      object? cachedQuery = this.service.QueryOnline(year);
      if (cachedQuery != null) {
        yearCache[year] = cachedQuery;
        var rate = this.service.Calculate(income, year, cachedQuery);
        if(rate != null){
          rate.ReliabilityEnum = Reliability.Online;
          return rate;
        }
      }
    }
    throw new Exception("Not found tax for this query");
  }
}