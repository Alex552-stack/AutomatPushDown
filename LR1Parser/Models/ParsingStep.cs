namespace Tema1Limbaje.Models;

public class ParsingStep
{
    public string Stack { get; set; }
    public string Input { get; set; }
    public string Action { get; set; }
    public string Result { get; set; }

    public ParsingStep(string stack, string input, string action, string result)
    {
        Stack = stack;
        Input = input;
        Action = action;
        Result = result;
    }
}
