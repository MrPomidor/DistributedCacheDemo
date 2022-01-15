namespace Common;

public class LargeDataItem
{
    public DateTime DateNow { get; set; }

    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Configuration { get; set; } = new();
}
