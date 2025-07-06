public interface IDriverMatcher
{
    Driver? FindNearestDriver(RideRequest request);
}

public class DriverMatcher : IDriverMatcher
{
    private readonly IDriverRepository _driverRepository;
    private readonly IDistanceCalculator _distanceCalculator;

    public DriverMatcher(IDriverRepository driverRepository, IDistanceCalculator distanceCalculator)
    {
        _driverRepository = driverRepository;
        _distanceCalculator = distanceCalculator;
    }

    public Driver? FindNearestDriver(RideRequest request)
    {
        var availableDrivers = _driverRepository.GetAvailableDrivers();

        return availableDrivers
            .OrderBy(d => _distanceCalculator.Between(d.CurrentLocation, request.PickupLocation))
            .FirstOrDefault();
    }
}
