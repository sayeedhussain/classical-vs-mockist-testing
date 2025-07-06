public class Driver
{
    public enum StatusEnum
    {
        Available,
        Booked
    }

    public string Id { get; }
    public Location CurrentLocation { get; }
    public StatusEnum Status { get; private set; }

    public Driver(string id, Location location, StatusEnum status = StatusEnum.Available)
    {
        Id = id;
        CurrentLocation = location;
        Status = status;
    }

    public void MarkAsBooked()
    {
        if (Status != StatusEnum.Available)
            throw new InvalidOperationException("Driver is not available.");

        Status = StatusEnum.Booked;
    }

    public bool IsAvailable() => Status == StatusEnum.Available;
}
