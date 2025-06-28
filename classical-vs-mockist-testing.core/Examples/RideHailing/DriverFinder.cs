public class DriverFinder : IDriverFinder
{
    private readonly List<Driver> _availableDrivers;
    private readonly DistanceCalculator _distanceCalculator;

    public DriverFinder(List<Driver> availableDrivers, DistanceCalculator distanceCalculator)
    {
        _availableDrivers = availableDrivers;
        _distanceCalculator = distanceCalculator;
    }

    public Driver FindNearestDriver(RideRequest request)
    {
        Driver nearest = null;
        double minDistance = double.MaxValue;

        foreach (var driver in _availableDrivers)
        {
            if (!driver.IsAvailable()) continue;

            double distance = _distanceCalculator.Between(driver.CurrentLocation, request.PickupLocation);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = driver;
            }
        }

        return nearest;
    }
}
