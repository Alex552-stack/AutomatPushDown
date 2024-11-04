

using System.Text.Json;
using Tema1Limbaje.Models;

InputModel model = 
    JsonSerializer.Deserialize<InputModel>(
        File.ReadAllText("input.json")
    );

ParsingTableInput parsingTableInput = JsonSerializer.Deserialize<ParsingTableInput>(File.ReadAllText("Table.json"));
ParsingTable parsingTable = new ParsingTable(parsingTableInput);

var parser = new Parser(parsingTable);
parser.Parse("i+i*i");
parser.PrintSteps();
