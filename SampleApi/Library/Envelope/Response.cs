namespace Library.Envelope;
using System.Text.Json.Serialization;


public class Response<T>
{
  public T? Result { get; set; }
  public bool Success { get; set; }
  public string Reliability { 
    get { return this.ReliabilityEnum.ToString(); }
  }

  [JsonIgnore]
  public Reliability ReliabilityEnum { get; set; }

}