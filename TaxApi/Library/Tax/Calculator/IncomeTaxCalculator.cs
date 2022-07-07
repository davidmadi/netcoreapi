namespace Library.Tax.Calculator;

public static class IncomeTaxCalculator
{
  public static IncomeTaxResult Calculate(int year, decimal income, List<Bracket> brackets)
  {
    var result = new IncomeTaxResult(year, income);
    var sorted = brackets.OrderBy(b => b.min).ToList();

    decimal remainingIncome = income;
    foreach(var currentBracked in sorted)
    {
      if (currentBracked.max > 0)
      {
        decimal taxable = currentBracked.max.Value - currentBracked.Min();
        if(remainingIncome > taxable){
          remainingIncome -= taxable;
        } else {
          taxable = remainingIncome;
        }
        
        //remove taxable from income
        result.incomeTaxPayableAmount += ApplyRateToAmount(currentBracked.rate, taxable);
      } else {
        result.incomeTaxPayableAmount += ApplyRateToAmount(currentBracked.rate, remainingIncome);
      }
      if (remainingIncome <= 0) break;
    }

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
