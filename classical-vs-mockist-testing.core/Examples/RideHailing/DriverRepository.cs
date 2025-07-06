using Microsoft.Data.SqlClient;

public class DriverRepository : IDriverRepository
{
    private readonly string _connectionString;
    
    public DriverRepository(string connectionString) => _connectionString = connectionString;
    
    public List<Driver> GetAvailableDrivers()
    {
        var drivers = new List<Driver>();
        
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Id, Latitude, Longitude FROM Drivers WHERE Status = 'Available'", connection);
        
        connection.Open();
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            var driver = new Driver(
                reader.GetString(0), 
                new Location(reader.GetDouble(1), reader.GetDouble(2))
            );
            drivers.Add(driver);
        }
        
        return drivers;
    }
}