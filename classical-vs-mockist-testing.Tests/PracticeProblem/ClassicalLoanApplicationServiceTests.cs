using Moq;
using FluentAssertions;

public class ClassicalLoanApplicationServiceTests
{
    [Fact]
    public void Evaluate_ShouldApproveLoan_WhenCreditScoreAndDtiAreEligible()
    {
        // Arrange
        var loanApplication = new LoanApplicationRequest(
            ApplicantId: "applicant-123",
            LoanAmount: 100000m,
            DurationMonths: 10);

        var mockCreditScoreService = new Mock<ICreditScoreService>();
        mockCreditScoreService.Setup(f => f.GetCreditScore(loanApplication.ApplicantId)).Returns(780);

        var mockIncomeService = new Mock<IIncomeService>();
        mockIncomeService.Setup(f => f.GetMonthlyIncome(loanApplication.ApplicantId)).Returns(50000m);

        var service = new LoanApplicationService(
            mockCreditScoreService.Object,
            mockIncomeService.Object,
            new LoanEligibilityPolicy(minCreditScore: 700, maxDtiRatio: 0.3m));

        // Act
        var result = service.Evaluate(loanApplication);

        // Assert
        result.Status.Should().Be(LoanStatus.Approved);
    }

    [Fact]
    public void Evalute_ShouldNotApproveLoan_WhenCreditScoreIsNotMeetingEligibilityPolicy()
    {
        // Arrange
        var loanApplication = new LoanApplicationRequest(
            ApplicantId: "applicant-123",
            LoanAmount: 100000m,
            DurationMonths: 10);

        var mockCreditScoreService = new Mock<ICreditScoreService>();
        mockCreditScoreService.Setup(f => f.GetCreditScore(loanApplication.ApplicantId)).Returns(650);

        var mockIncomeService = new Mock<IIncomeService>();
        mockIncomeService.Setup(f => f.GetMonthlyIncome(loanApplication.ApplicantId)).Returns(50000m);

        var service = new LoanApplicationService(
            mockCreditScoreService.Object,
            mockIncomeService.Object,
            new LoanEligibilityPolicy(minCreditScore: 700, maxDtiRatio: 0.3m));

        // Act
        var result = service.Evaluate(loanApplication);

        // Assert
        result.Status.Should().Be(LoanStatus.Rejected);
    }

    [Fact]
    public void Evalute_ShouldNotApproveLoan_WhenDtiIsNotMeetingEligibilityPolicy()
    {
        // Arrange
        var loanApplication = new LoanApplicationRequest(
            ApplicantId: "applicant-123",
            LoanAmount: 1000000m,
            DurationMonths: 10);

        var mockCreditScoreService = new Mock<ICreditScoreService>();
        mockCreditScoreService.Setup(f => f.GetCreditScore(loanApplication.ApplicantId)).Returns(720);

        var mockIncomeService = new Mock<IIncomeService>();
        mockIncomeService.Setup(f => f.GetMonthlyIncome(loanApplication.ApplicantId)).Returns(50000m);

        var service = new LoanApplicationService(
            mockCreditScoreService.Object,
            mockIncomeService.Object,
            new LoanEligibilityPolicy(minCreditScore: 700, maxDtiRatio: 0.3m));

        // Act
        var result = service.Evaluate(loanApplication);

        // Assert
        result.Status.Should().Be(LoanStatus.Rejected);
    }

}