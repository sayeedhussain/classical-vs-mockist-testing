using Xunit;
using FluentAssertions;

using Moq;

public class ClassicalRideBookingServiceTests
{
    [Fact]
    public void AssignDriver_ShouldReturnNearestAvailableDriver_AndMarkAsBooked()
    {
        // Arrange
        var drivers = new List<Driver>
        {
            new Driver("d1", new Location(12.9611, 77.6387)),  // closest
            new Driver("d2", new Location(12.9615, 77.6412)),
            new Driver("d3", new Location(12.9550, 77.6300))
        };

        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(f => f.GetAvailableDrivers()).Returns(drivers);

        var request = new RideRequest("user42", new Location(12.9610, 77.6390));

        var distanceCalculator = new DistanceCalculator();
        var driverFinder = new DriverMatcher(mockDriverRepository.Object, distanceCalculator);
        var rideBookingService = new RideBookingService(driverFinder);

        // Act
        var assigned = rideBookingService.AssignDriver(request);

        // Assert
        assigned.Should().NotBeNull();
        assigned.Id.Should().Be("d1");
        assigned.Status.Should().Be(Driver.StatusEnum.Booked);
    }
}
