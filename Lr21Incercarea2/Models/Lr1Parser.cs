using ConsoleTables;
using Lr21Incercarea2.Tables;

namespace Tema1Limbaje.Models;

public static class Lr1Parser
{
    public static void Parse(InputModel model, Table actionTable, Table goToTable, string input = "i+i*i")
    {
        Stack<(char, int)> stack = new();
        stack.Push(('$',0));
        input += '$';
        var table = new ConsoleTable("Stiva", "cuvant de intrare", "actiune rezultata");
        while (true)
        {
            var currentS = stack.Peek();
            var currentIn = input[0];

            var actionKey = (currentS.Item2.ToString(), currentIn.ToString());
            var action = actionTable[actionKey];
            table.AddRow(
                string.Join(" ", stack.ToArray().Reverse().Select(el => $"{el.Item1}:{el.Item2}")), 
                input, 
                action
                );
            
            if (action[0] == 'd')
            {   //d adauga pe stiva
                stack.Push((currentIn,int.Parse(action[1..])));
                input = input[1..];
            }
            else if (action[0] == 'r')
            {
                // Reduce action: get the production rule number
                int productionNumber = int.Parse(action[1..]);
                var production = model.Productions[productionNumber - 1];

                // Pop one element from the stack
                for (int i = 0; i < production.Right.Length; i++)
                {
                    stack.Pop();
                }

                // Get the state from the top of the stack
                var topState = stack.Peek().Item2;

                // Get the non-terminal from the production rule
                var nonTerminal = production.Left[0];

                // Get the new state from the goto table
                var gotoKey = (topState.ToString(), nonTerminal.ToString());
                var newState = int.Parse(goToTable[gotoKey]);

                // Push the non-terminal and the new state onto the stack
                stack.Push((nonTerminal, newState));
            }
            else if (action[0] == 'a')
            {
                // Accept action: parsing is complete
                Console.WriteLine("Input accepted.");
                return;
            }
            else
            {
                throw new Exception("S-a dus pe pl cu caracter-ul " + action);
            }
            table.Write();
        }
    }
}