public interface ILoanEligibilityPolicy
{
    bool Evaluate(LoanApplication loan);
}

public class LoanEligibilityPolicy : ILoanEligibilityPolicy
{
    private readonly int _minCreditScore;
    private readonly decimal _maxDti;

    public LoanEligibilityPolicy(int minCreditScore, decimal maxDtiRatio)
    {
        _minCreditScore = minCreditScore;
        _maxDti = maxDtiRatio;
    }

    public bool Evaluate(LoanApplication loan)
    {
        return loan.CreditScore >= _minCreditScore && loan.DtiRatio <= _maxDti;
    }
}
