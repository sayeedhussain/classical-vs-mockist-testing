public class LoanApplication
{
    public string ApplicantId { get; }
    public decimal LoanAmount { get; }
    public int DurationMonths { get; }
    public int CreditScore { get; }
    public decimal MonthlyIncome { get; }

    public decimal MonthlyEmi => LoanAmount / DurationMonths;
    public decimal DtiRatio => MonthlyEmi / MonthlyIncome;

    public LoanApplication(
        string applicantId,
        decimal loanAmount,
        int durationMonths,
        int creditScore,
        decimal monthlyIncome
    )
    {
        ApplicantId = applicantId;
        LoanAmount = loanAmount;
        DurationMonths = durationMonths;
        CreditScore = creditScore;
        MonthlyIncome = monthlyIncome;
    }
}
