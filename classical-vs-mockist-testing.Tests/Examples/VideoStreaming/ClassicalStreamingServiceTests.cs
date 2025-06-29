using Xunit;
using FluentAssertions;

public class ClassicalStreamingServiceTests
{
    [Fact]
    public void StreamShow_ShouldUpdateHistoryAndTriggerRecommendations()
    {
        var user = new User("Bob");
        var show = new Show("Dark", new List<string> { "thriller", "sci-fi" });
        var catalog = new List<Show>
        {
            new Show("Stranger Things", new List<string> { "thriller", "80s" }),
            new Show("Black Mirror", new List<string> { "sci-fi", "dystopia" }),
            new Show("The Crown", new List<string> { "drama", "history" }),
        };
        var engine = new RecommendationEngine(catalog);
        var service = new StreamingService(engine);

        service.StreamShow(user, show);

        user.WatchHistory.Should().Contain(show);
        user.PreferredTags().Should().Contain("thriller").And.Contain("sci-fi");
    }
}
