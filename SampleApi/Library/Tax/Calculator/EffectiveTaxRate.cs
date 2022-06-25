namespace Library.Tax.Calculator;
using System.Text.Json.Serialization;
using Library.Tax.Services;

public class EffectiveTaxRate {
  public float Income { get; set; }
  public float Rate { get; set; }
  public int Year { get; set; }

  public string Reliability { 
    get { return this.ReliabilityEnum.ToString(); }
  }

  [JsonIgnore]
  public Reliability ReliabilityEnum { get; set; }


}