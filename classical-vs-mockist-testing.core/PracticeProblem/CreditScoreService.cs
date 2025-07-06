using System.Net.Http.Json;

public interface ICreditScoreService
{
    public int GetCreditScore(string applicantId);
}

public class CreditScoreService : ICreditScoreService
{
    private readonly HttpClient _httpClient;

    public CreditScoreService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public int GetCreditScore(string applicantId)
    {
        try
        {
            var response = _httpClient
                .GetAsync($"/api/credit-score/{applicantId}")
                .Result;

            response.EnsureSuccessStatusCode(); // May throw HttpRequestException

            var result = response.Content.ReadFromJsonAsync<CreditScoreResponse>().Result;

            return result?.Score ?? throw new Exception("Invalid credit score response");
        }
        catch (HttpRequestException)
        {
            throw new Exception("Invalid credit score response");
        }
        catch (NotSupportedException)
        {
            throw new Exception("Invalid credit score response");
        }
        catch (Exception)
        {
            throw new Exception("Invalid credit score response");
        }
    }

    private class CreditScoreResponse
    {
        public int Score { get; set; }
    }
}