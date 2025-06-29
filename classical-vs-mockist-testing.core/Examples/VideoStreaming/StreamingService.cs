public class StreamingService
{
    private readonly RecommendationEngine _engine;

    public StreamingService(RecommendationEngine engine)
    {
        _engine = engine;
    }

    public void StreamShow(User user, Show show)
    {
        user.Watch(show);

        var updatedRecommendations = _engine.GetRecommendations(user);
        //display recommended shows to user once streaming of current show is complete
    }
}
