public class RideBookingService
{
    private readonly IDriverMatcher _driverMatcher;

    public RideBookingService(IDriverMatcher driverMatcher)
    {
        _driverMatcher = driverMatcher;
    }

    public Driver AssignDriver(RideRequest request)
    {
        var driver = _driverMatcher.FindNearestDriver(request);
        
        if (driver == null)
            throw new Exception("No available drivers");

        driver.MarkAsBooked();
        return driver;
    }
}
