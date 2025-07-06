using Xunit;
using Moq;
using FluentAssertions;

public class MockistRideBookingServiceTests
{
    [Fact]
    public void AssignDriver_ShouldReturnNearestAvailableDriver_AndMarkAsBooked()
    {
        // Arrange
        var request = new RideRequest("user99", new Location(12.9610, 77.6390));
        var driver = new Driver("mock-driver", new Location(12.9612, 77.6389));

        var mockDriverMatcher = new Mock<IDriverMatcher>();
        mockDriverMatcher.Setup(f => f.FindNearestDriver(request)).Returns(driver);

        var rideBookingService = new RideBookingService(mockDriverMatcher.Object);

        // Act
        var assigned = rideBookingService.AssignDriver(request);

        // Assert
        assigned.Should().BeSameAs(driver);
        assigned.Status.Should().Be(Driver.StatusEnum.Booked);
        mockDriverMatcher.Verify(f => f.FindNearestDriver(request), Times.Once);
    }

        [Fact]
    public void AssignDriver_ShouldReturnNoDriver_WhenNoDriverAvailable()
    {
        // Arrange
        var request = new RideRequest("user99", new Location(12.9610, 77.6390));

        var mockDriverMatcher = new Mock<IDriverMatcher>();
        mockDriverMatcher.Setup(f => f.FindNearestDriver(request)).Returns(() => null);

        var rideBookingService = new RideBookingService(mockDriverMatcher.Object);

        // Act
        var assigned = rideBookingService.AssignDriver(request);

        // Assert
        assigned.Should().BeNull();
        mockDriverMatcher.Verify(f => f.FindNearestDriver(request), Times.Once);
    }

}