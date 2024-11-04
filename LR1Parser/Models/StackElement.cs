namespace Tema1Limbaje.Models;

public class StackElement
{
    public string Symbol { get; set; }
    public int State { get; set; }

    public StackElement(string symbol, int state)
    {
        Symbol = symbol;
        State = state;
    }
}
