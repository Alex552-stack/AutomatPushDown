namespace Tema1Limbaje.Models;

public class ParsingTableInput
{
    public Dictionary<string, string> ActionTable { get; set; } = new();
    public Dictionary<string, int> GotoTable { get; set; } = new();

}