namespace Lr21Incercarea2.Tables;

public class Table
{
    private Dictionary<(string, string), string> _table { get; set; } = new();
    
    public string this[(string, string) key]
    {
        get => _table[key];
        set => _table[key] = value;
    }

    public Table(Dictionary<string, string> input)
    {
        foreach (var pair in input) 
        {
            var keyParts = pair.Key.Split(", ");
            var key = (keyParts[0][1..], keyParts[1][..^1]);
            _table.Add(key, pair.Value);
        }
    }
}