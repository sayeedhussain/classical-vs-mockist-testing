public class RecommendationEngine
{
    private readonly List<Show> _catalog;

    public RecommendationEngine(List<Show> catalog)
    {
        _catalog = catalog;
    }

    public virtual List<Show> GetRecommendations(User user)
    {
        var preferredTags = user.PreferredTags();
        return _catalog
            .Where(show => show.Tags.Any(tag => preferredTags.Contains(tag)))
            .Take(5)
            .ToList();
    }
}
