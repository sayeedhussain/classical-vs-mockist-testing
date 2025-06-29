public class User
{
    public string Name { get; }
    public List<Show> WatchHistory { get; } = new();
    private Dictionary<string, int> TagPreferenceCount { get; } = new();

    public User(string name)
    {
        Name = name;
    }

    public virtual void Watch(Show show)
    {
        WatchHistory.Add(show);
        foreach (var tag in show.Tags)
        {
            if (!TagPreferenceCount.ContainsKey(tag))
                TagPreferenceCount[tag] = 0;
            TagPreferenceCount[tag]++;
        }
    }

    public virtual List<string> PreferredTags(int topN = 3)
    {
        return TagPreferenceCount
            .OrderByDescending(kv => kv.Value)
            .Take(topN)
            .Select(kv => kv.Key)
            .ToList();
    }
}
