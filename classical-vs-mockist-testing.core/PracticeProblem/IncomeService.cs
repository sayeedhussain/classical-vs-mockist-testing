using System.Net.Http.Json;

public interface IIncomeService
{
    public decimal GetMonthlyIncome(string applicantId);

}

public class IncomeService : IIncomeService
{
    private readonly HttpClient _httpClient;

    public IncomeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public decimal GetMonthlyIncome(string applicantId)
    {
        try
        {
        var response = _httpClient
            .GetAsync($"/v1/c3aa98f6-7aec-42eb-903a-95edabf28980")
            .Result;

        response.EnsureSuccessStatusCode();

        var result = response.Content.ReadFromJsonAsync<MonthlyIncomeResponse>().Result;

        return result?.IncomeDetails?.MonthlyIncome ?? throw new Exception("Invalid monthly income response");
        }
        catch (HttpRequestException)
        {
            throw new Exception("Invalid monthly income response");
        }
        catch (NotSupportedException)
        {
            throw new Exception("Invalid monthly income response");
        }
        catch (Exception)
        {
            throw new Exception("Invalid monthly income response");
        }
    }

    private class MonthlyIncomeResponse
    {
        public string ApplicantId { get; set; }
        public IncomeDetails IncomeDetails { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }

    private class IncomeDetails
    {
        public string EmploymentType { get; set; }
        public string EmployerName { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string Currency { get; set; }
        public bool IncomeVerified { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}