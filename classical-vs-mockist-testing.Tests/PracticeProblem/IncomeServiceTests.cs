using Xunit;
using FluentAssertions;
using Moq;
using System.Net;
using System.Text;

public class IncomeServiceTests
{
    [Fact]
    public void GetMonthlyIncome_ShouldReturnScore_WhenApiReturnsSuccess()
    {
        // Arrange
        var expectedIncome = 50000m;
        var json = $"{{\"applicantId\":\"applicant-123\",\"incomeDetails\":{{\"employmentType\":\"Salaried\",\"employerName\":\"TechCorp Inc.\",\"monthlyIncome\":{expectedIncome},\"currency\":\"INR\",\"incomeVerified\":true,\"lastUpdated\":\"2025-07-07T10:30:00Z\"}},\"status\":\"SUCCESS\",\"timestamp\":\"2025-07-07T10:31:12Z\"}}";

        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://income.example.com")
        };

        var service = new IncomeService(httpClient);

        // Act
        var income = service.GetMonthlyIncome("applicant-123");

        // Assert
        income.Should().Be(expectedIncome);
        handler.LastRequest!.RequestUri!.ToString().Should().Contain("/v1/c3aa98f6-7aec-42eb-903a-95edabf28980");
    }

    [Fact]
    public void GetMonthlyIncome_ShouldThrow_WhenApiResponseIsInvalid()
    {
        // Arrange: no content = null result
        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK); // No Content

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://income.example.com")
        };

        var service = new IncomeService(httpClient);

        // Act
        Action act = () => service.GetMonthlyIncome("applicant-123");

        // Assert
        act.Should().Throw<Exception>().WithMessage("Invalid monthly income response");
    }
}
