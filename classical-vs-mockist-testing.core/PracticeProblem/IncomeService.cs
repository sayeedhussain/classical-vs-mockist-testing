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
            .GetAsync($"/api/monthly-income/{applicantId}")
            .Result;

        response.EnsureSuccessStatusCode();

        var result = response.Content.ReadFromJsonAsync<MonthlyIncomeResponse>().Result;

        return result?.Income ?? throw new Exception("Invalid monthly income response");
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
        public int Income { get; set; }
    }

}