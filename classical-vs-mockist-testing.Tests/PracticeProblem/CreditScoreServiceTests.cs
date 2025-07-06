using Xunit;
using FluentAssertions;
using Moq;
using System.Net;
using System.Text;

public class CreditScoreServiceTests
{
    [Fact]
    public void GetCreditScore_ShouldReturnScore_WhenApiReturnsSuccess()
    {
        // Arrange
        var expectedScore = 720;
        var json = "{\"score\":720}";

        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://credit.example.com")
        };

        var service = new CreditScoreService(httpClient);

        // Act
        var score = service.GetCreditScore("applicant-123");

        // Assert
        score.Should().Be(expectedScore);
        handler.LastRequest!.RequestUri!.ToString().Should().Contain("/api/credit-score/applicant-123");
    }

    [Fact]
    public void GetCreditScore_ShouldThrow_WhenApiResponseIsInvalid()
    {
        // Arrange: no content = null result
        var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK); // No Content

        var handler = new StubHttpMessageHandler(fakeResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://credit.example.com")
        };

        var service = new CreditScoreService(httpClient);

        // Act
        Action act = () => service.GetCreditScore("applicant-123");

        // Assert
        act.Should().Throw<Exception>().WithMessage("Invalid credit score response");
    }
}
