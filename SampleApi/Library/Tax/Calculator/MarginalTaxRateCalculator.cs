namespace Library.Tax.Calculator;

public static class MarginalTaxRateCalculator
{
  public static MarginalTaxResult Calculate(decimal income, decimal raise, List<Bracket> brackets)
  {
    var result = new MarginalTaxResult();
    var sorted = brackets.OrderBy(b => b.min).ToList();
    var incomeBraket = MarginalTaxRateCalculator.FindBracketByIncome(income, brackets);
    if(incomeBraket != null) {
      result.incomeTaxes = incomeBraket.rate * income;
      if (raise > 0 && incomeBraket.max > 0) {
        decimal taxableWindow = incomeBraket.max.Value - income;
        result.windowToMaxTaxes = taxableWindow * incomeBraket.rate;
        decimal raiseTaxable = raise - taxableWindow;
        var raiseBracket = MarginalTaxRateCalculator.FindBracketByIncome(income+raise, brackets);
        if (raiseBracket != null) {
          result.raiseTaxes = raiseBracket.rate * raiseTaxable;
        }
      }
    }
    result.marginalTaxes = Math.Round(result.windowToMaxTaxes + result.raiseTaxes, 2);
    result.income = income;
    result.raise = raise;

    return result;
  }

  public static Bracket? FindBracketByIncome(decimal amount, List<Bracket> bracketList) {
    foreach(var bracket in bracketList) {
      if (bracket.min != null && bracket.max != null){
        if (bracket.min <= amount && amount < bracket.max){
          return bracket;
        }
      } else if (bracket.min != null) {
        if (bracket.min <= amount){
          return bracket;
        }
      }
      else if (bracket.max != null) {
        if (bracket.max > amount){ //2021 rule
          return bracket;
        }
      }
    }
    return null;
  }

}
