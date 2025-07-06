using Xunit;
using FluentAssertions;
using System;

public class DistanceCalculatorTests
{
        private readonly DistanceCalculator _calculator;

        public DistanceCalculatorTests()
        {
            _calculator = new DistanceCalculator();
        }

        [Fact]
        public void Between_SameLocation_ShouldReturnZero()
        {
            // Arrange
            var location = new Location(12.9716, 77.5946);

            // Act
            var result = _calculator.Between(location, location);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void Between_DifferentLocations_ShouldReturnEuclideanDistance()
        {
            // Arrange
            var location1 = new Location(0, 0);
            var location2 = new Location(3, 4);

            // Act
            var result = _calculator.Between(location1, location2);

            // Assert
            result.Should().Be(5.0); // 3-4-5 right triangle
        }

        [Fact]
        public void BetweenHaversine_SameLocation_ShouldReturnZero()
        {
            // Arrange
            var location = new Location(12.9716, 77.5946);

            // Act
            var result = _calculator.BetweenHaversine(location, location);

            // Assert
            result.Should().Be(0);
        }


        [Fact]
        public void BetweenHaversine_KnownDistance_ShouldCalculateAccurately()
        {
            // Arrange - Distance between London and Paris (known distance ≈ 344 km)
            var london = new Location(51.5074, -0.1278);
            var paris = new Location(48.8566, 2.3522);

            // Act
            var result = _calculator.BetweenHaversine(london, paris);

            // Assert
            result.Should().BeApproximately(344000, 5000); // Within 5km tolerance
        }

}