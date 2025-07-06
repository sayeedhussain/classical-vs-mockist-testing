public interface IDriverMatcher
{
    Driver? FindNearestDriver(RideRequest request);
}
