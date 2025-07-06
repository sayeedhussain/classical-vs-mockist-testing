using Xunit;
using Moq;
using FluentAssertions;

public class MockistStreamingServiceTests
{
    [Fact]
    public void StreamShow_ShouldCallWatchAndGetRecommendations()
    {
        //Arrange
        var show = new Show("Dark", new List<string> { "thriller", "sci-fi" });

        var mockUser = new Mock<User>("Bob");
        var mockEngine = new Mock<RecommendationEngine>(new List<Show>());

        var service = new StreamingService(mockEngine.Object);

        //Act
        service.StreamShow(mockUser.Object, show);

        //Assert
        mockUser.Verify(u => u.Watch(show), Times.Once);
        mockEngine.Verify(e => e.GetRecommendations(mockUser.Object), Times.Once);
    }
}
