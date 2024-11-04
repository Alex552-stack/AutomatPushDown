namespace Tema1Limbaje.Models;

public class InputModel
{
    public List<Production> Productions { get; set; } = new();
    public HashSet<string> NonTerminals { get; set; } = new();
    public HashSet<string> Terminals { get; set; } = new();
    public string StartSymbol { get; set; } = "";
}

public record Production
{
    public string Left { get; set; }
    public string Right { get; set; }
}