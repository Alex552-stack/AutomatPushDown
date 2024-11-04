using Tema1Limbaje.Models;

namespace AutomatPushDown.Utils;

public class PushDown(InputModel data)
{
    public InputModel data = data;

    public void CalculateTinis()
    {
        Dictionary<string, HashSet<string>> TINI = new();

        while (TINI.Keys.Count < data.NonTerminals.Count)
        {
            var current = data.Productions.Keys.First(k => !TINI.ContainsKey(k));
            TINI.Add(current, []);
            var candidates = data.Productions[current].Select(str => str[0]).ToList();
            candidates.RemoveAll(c => c.ToString() == current);

            foreach (var candidate in candidates)
            {
                TINI[current].Add(candidate.ToString());
            }
        }

        //ai bullshit
        // Second pass: Resolve non-terminals in TINI
        foreach (var nonTerminal in data.NonTerminals)
        {
            var currentTINI = new HashSet<string>(TINI[nonTerminal]);

            foreach (var subSymbol 
                     in currentTINI.Where(symbol => 
                                                data.NonTerminals.Contains(symbol))
                                   .SelectMany(symbol => TINI[symbol]))
            {
                TINI[nonTerminal].Add(subSymbol);
            }
        }
        Console.WriteLine();
    }
}