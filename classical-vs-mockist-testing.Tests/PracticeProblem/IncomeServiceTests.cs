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
        var expectedIncome = 50000;
        var json = "{\"income\":50000}";

        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://credit.example.com")
        };

        var service = new IncomeService(httpClient);

        // Act
        var income = service.GetMonthlyIncome("applicant-123");

        // Assert
        income.Should().Be(expectedIncome);
        handler.LastRequest!.RequestUri!.ToString().Should().Contain("/api/monthly-income/applicant-123");
    }

    [Fact]
    public void GetMonthlyIncome_ShouldThrow_WhenApiResponseIsInvalid()
    {
        // Arrange: no content = null result
        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK); // No Content

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://credit.example.com")
        };

        var service = new IncomeService(httpClient);

        // Act
        Action act = () => service.GetMonthlyIncome("applicant-123");

        // Assert
        act.Should().Throw<Exception>().WithMessage("Invalid monthly income response");
    }
}
