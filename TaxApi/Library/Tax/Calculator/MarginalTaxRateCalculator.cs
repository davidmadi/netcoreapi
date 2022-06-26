namespace Library.Tax.Calculator;

public static class MarginalTaxRateCalculator
{
  public static IncomeTaxResult Calculate(decimal income, decimal raise, List<Bracket> brackets)
  {
    var result = new IncomeTaxResult();
    var sorted = brackets.OrderBy(b => b.min).ToList();
    var incomeBraket = MarginalTaxRateCalculator.FindBracketByIncome(income, brackets);
    if(incomeBraket != null) {
      result.incomeTaxPayableAmount = ApplyRateToAmount(incomeBraket.rate, income);
      if (raise > 0 && incomeBraket.max > 0) {
        decimal taxableWindow = incomeBraket.max.Value - income;
        result.maxThresholdPayableAmount = ApplyRateToAmount(incomeBraket.rate, taxableWindow);
        decimal raiseTaxable = raise - taxableWindow;
        var raiseBracket = MarginalTaxRateCalculator.FindBracketByIncome(income+raise, brackets);
        if (raiseBracket != null) {
          result.raiseTaxes = ApplyRateToAmount(raiseBracket.rate, raiseTaxable);
        }
      } else {
        result.raiseTaxes = ApplyRateToAmount(raise, incomeBraket.rate);
      }
    }
    result.marginalTaxPayableAmount = Math.Round(result.maxThresholdPayableAmount + result.raiseTaxes, 2);
    result.income = income;
    result.raise = raise;

    return result;
  }

  private static decimal ApplyRateToAmount(decimal rate, decimal amount) {
    return Math.Round(rate * amount, 2);
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
