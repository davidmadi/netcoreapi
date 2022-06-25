public class IncomeResponse {
  public List<Bracket>? tax_brackets {get;set;}

  public List<IncomeError>? errors {get;set;}
}

public class IncomeError {
  public string? field { get; set; }
  public string? code { get; set; }
  public string? message { get; set; }
}