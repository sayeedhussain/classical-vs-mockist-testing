public class DriverMatcher : IDriverMatcher
{
    private readonly IDriverRepository _driverRepository;
    private readonly DistanceCalculator _distanceCalculator;

    public DriverMatcher(IDriverRepository driverRepository, DistanceCalculator distanceCalculator)
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
