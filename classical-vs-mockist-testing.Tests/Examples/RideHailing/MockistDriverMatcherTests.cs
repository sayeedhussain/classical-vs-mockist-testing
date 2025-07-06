using Xunit;
using Moq;
using FluentAssertions;

public class MockistDriverMatcherTests
{
    [Fact]
    public void FindNearestDriver_ShouldReturnNearestAvailableDriver()
    {
        // Arrange
        var request = new RideRequest("user99", new Location(12.9610, 77.6390));
        var driver1 = new Driver("mock-driver", new Location(12.9612, 77.6389));
        var driver2 = new Driver("mock-driver", new Location(13.9612, 77.6389));

        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(f => f.GetAvailableDrivers()).Returns(() => new List<Driver> { driver1, driver2 });

        var mockDistanceCalculator = new Mock<IDistanceCalculator>();
        mockDistanceCalculator.Setup(d => d.Between(driver1.CurrentLocation, request.PickupLocation)).Returns(() => 5);
        mockDistanceCalculator.Setup(d => d.Between(driver2.CurrentLocation, request.PickupLocation)).Returns(() => 10);

        var driverMatcher = new DriverMatcher(mockDriverRepository.Object, mockDistanceCalculator.Object);

        // Act
        var driver = driverMatcher.FindNearestDriver(request);

        // Assert
        driver.Should().BeSameAs(driver1);
        mockDistanceCalculator.Verify(d => d.Between(It.IsAny<Location>(), It.IsAny<Location>()), Times.Exactly(2));
    }

}