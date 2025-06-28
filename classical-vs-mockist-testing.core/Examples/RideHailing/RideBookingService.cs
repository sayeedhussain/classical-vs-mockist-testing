public class RideBookingService
{
    private readonly IDriverFinder _driverFinder;

    public RideBookingService(IDriverFinder driverFinder)
    {
        _driverFinder = driverFinder;
    }

    public Driver AssignDriver(RideRequest request)
    {
        var driver = _driverFinder.FindNearestDriver(request);
        if (driver == null)
            throw new Exception("No available drivers");

        driver.MarkAsBooked();
        return driver;
    }
}
