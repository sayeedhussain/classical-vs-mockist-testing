public class RideRequest
{
    public string UserId { get; }
    public Location PickupLocation { get; }

    public RideRequest(string userId, Location pickupLocation)
    {
        UserId = userId;
        PickupLocation = pickupLocation;
    }
}
