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
                .GetAsync($"/v1/62d1604c-2fa9-4163-832d-3347d8e4d730")
                .Result;

            response.EnsureSuccessStatusCode(); // May throw HttpRequestException

            var result = response.Content.ReadFromJsonAsync<CreditScoreResponse>().Result;

            return result?.CreditScore?.Score ?? throw new Exception("Invalid credit score response");
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
        public string ApplicantId { get; set; }
        public CreditScore CreditScore { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }

    private class CreditScore
    {
        public string Provider { get; set; }
        public int Score { get; set; }
        public ScoreRange ScoreRange { get; set; }
        public DateTime LastPulled { get; set; }
        public string ScoreStatus { get; set; }
    }

    private class ScoreRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

}