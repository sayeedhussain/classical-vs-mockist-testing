using Xunit;
using Moq;
using FluentAssertions;

public class MockistRideBookingServiceTests
{
    [Fact]
    public void AssignDriver_ShouldMarkDriverAsBooked_AndReturnIt()
    {
        // Arrange
        var request = new RideRequest("user99", new Location(12.9610, 77.6390));
        var driver = new Driver("mock-driver", new Location(12.9612, 77.6389));

        var mockFinder = new Mock<IDriverFinder>();
        mockFinder.Setup(f => f.FindNearestDriver(request)).Returns(driver);

        var rideBookingService = new RideBookingService(mockFinder.Object);

        // Act
        var assigned = rideBookingService.AssignDriver(request);

        // Assert
        assigned.Should().BeSameAs(driver);
        assigned.Status.Should().Be(Driver.StatusEnum.Booked);
        mockFinder.Verify(f => f.FindNearestDriver(request), Times.Once);
    }
}
