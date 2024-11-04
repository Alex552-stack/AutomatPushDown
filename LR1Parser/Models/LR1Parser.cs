using Tema1Limbaje.Models;

public class Parser
{
    private ParsingTable _parsingTable;
    private List<ParsingStep> _steps;

    public Parser(ParsingTable parsingTable)
    {
        _parsingTable = parsingTable;
        _steps = new List<ParsingStep>();
    }

    public void Parse(string input)
    {
        var stack = new Stack<(int State, string Symbol)>();
        stack.Push((0, "$"));
        input += "$";

        int pointer = 0;
        while (true)
        {
            int state = stack.Peek().State;
            string symbol = input[pointer].ToString();

            if (_parsingTable.ActionTable.TryGetValue((state, symbol), out var action))
            {
                if (action.StartsWith("d")) // shift action
                {
                    int nextState = int.Parse(action.Substring(1));
                    stack.Push((nextState, symbol));
                    pointer++;

                    _steps.Add(new ParsingStep(
                        string.Join("", stack),
                        input.Substring(pointer),
                        action,
                        ""
                    ));
                }
                else if (action.StartsWith("r")) // reduce action
                {
                    int ruleNumber = int.Parse(action.Substring(1));
                    var production = GetProduction(ruleNumber);

                    // Check if the stack has enough items to pop
                    if (stack.Count <= production.Right.Length)
                    {
                        throw new InvalidOperationException("Insufficient elements in stack for reduction.");
                    }

                    // Pop the necessary number of items from the stack
                    for (int i = 0; i < production.Right.Length - 1; i++)
                    {
                        stack.Pop();
                    }

                    // Check if there's still something in the stack after popping
                    if (stack.Count > 0)
                    {
                        var newState = stack.Peek().State; // Safely peek at the new state
                        string leftSymbol = production.Left;

                        // Validate the Goto table lookup
                        if (!_parsingTable.GotoTable.TryGetValue((newState, leftSymbol), out var nextState))
                        {
                            throw new InvalidOperationException("Invalid Goto table lookup for state " + newState +
                                                                " and symbol " + leftSymbol);
                        }

                        // Push the new state and left symbol onto the stack
                        stack.Push((nextState, leftSymbol));

                        // Add to the steps with the correct current state and action
                        _steps.Add(new ParsingStep(
                            string.Join("", stack),
                            input.Substring(pointer),
                            action,
                            $"{leftSymbol} => {string.Join(" ", production.Right)}" // Use Join for clear formatting
                        ));
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            "Stack is empty after reduction; cannot determine new state.");
                    }
                }
                else if (action == "acc") // accept
                {
                    _steps.Add(new ParsingStep(
                        string.Join("", stack),
                        input.Substring(pointer),
                        "accept",
                        "Expression accepted"
                    ));
                    break;
                }
            }
            else
            {
                _steps.Add(new ParsingStep(
                    string.Join("", stack),
                    input.Substring(pointer),
                    "error",
                    "Syntax error"
                ));
                break;
            }
        }
    }


    private (string Left, string Right) GetProduction(int ruleNumber)
    {
        // Dummy method to get production rule by number
        return ruleNumber switch
        {
            1 => ("E", "E+T"),
            2 => ("E", "T"),
            3 => ("T", "T*F"),
            4 => ("T", "F"),
            5 => ("F", "(E)"),
            6 => ("F", "id"),
            _ => throw new Exception("Invalid rule number")
        };
    }

    public void PrintSteps()
    {
        Console.WriteLine("Stiva \t\t Cuvant de intrare \t Actiune \t Rezultat");
        foreach (var step in _steps)
        {
            Console.WriteLine($"{step.Stack} \t {step.Input} \t {step.Action} \t {step.Result}");
        }
    }
}