public class Show
{
    public string Title { get; }
    public List<string> Tags { get; }

    public Show(string title, List<string> tags)
    {
        Title = title;
        Tags = tags;
    }
}
