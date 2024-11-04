namespace Tema1Limbaje.Models;

public class ParsingTable
{
    public Dictionary<(int, string), string> ActionTable { get; set; }
    public Dictionary<(int, string), int> GotoTable { get; set; }

    public ParsingTable()
    {
        ActionTable = new Dictionary<(int, string), string>();
        GotoTable = new Dictionary<(int, string), int>();
    }
    public ParsingTable(ParsingTableInput input)
    {
        ActionTable = new Dictionary<(int, string), string>();
        GotoTable = new Dictionary<(int, string), int>();

        foreach (var entry in input.ActionTable)
        {
            var keyParts = entry.Key.Trim('(', ')').Split(", ");
            int state = int.Parse(keyParts[0]);
            string symbol = keyParts[1];
            ActionTable[(state, symbol)] = entry.Value;
        }

        foreach (var entry in input.GotoTable)
        {
            var keyParts = entry.Key.Trim('(', ')').Split(", ");
            int state = int.Parse(keyParts[0]);
            string nonTerminal = keyParts[1];
            GotoTable[(state, nonTerminal)] = entry.Value;
        }
    }

}
