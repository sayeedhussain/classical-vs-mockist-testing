public class LoanApplicationService
{
    private readonly ICreditScoreService _creditScoreService;
    private readonly IIncomeService _incomeService;
    private readonly ILoanEligibilityPolicy _policy;

    public LoanApplicationService(
        ICreditScoreService creditScoreService,
        IIncomeService incomeService,
        ILoanEligibilityPolicy policy)
    {
        _creditScoreService = creditScoreService;
        _incomeService = incomeService;
        _policy = policy;
    }

    public LoanApplicationResult Evaluate(LoanApplicationRequest request)
    {
        var score = _creditScoreService.GetCreditScore(request.ApplicantId);
        var income = _incomeService.GetMonthlyIncome(request.ApplicantId);

        var loan = new LoanApplication(request.ApplicantId, request.LoanAmount, request.DurationMonths, score, income);
        var status = _policy.Evaluate(loan) ? LoanStatus.Approved : LoanStatus.Rejected;

        return new LoanApplicationResult(request.ApplicantId, status);
    }
}
