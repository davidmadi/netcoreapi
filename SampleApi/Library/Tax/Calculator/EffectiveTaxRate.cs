namespace Library.Tax.Calculator;
using System.Text.Json.Serialization;
using Library.Tax.Services;

public class EffectiveTaxRate {
  public decimal Income { get; set; }
  public decimal? Rate { get; set; }
  public int Year { get; set; }

  [JsonIgnore]
  public string Reliability { 
    get { return this.ReliabilityEnum.ToString(); }
  }

  [JsonIgnore]
  public Reliability ReliabilityEnum { get; set; }


}