using System.Text.Json;
using Lr21Incercarea2.Tables;
using Tema1Limbaje.Models;


InputModel model = JsonSerializer.Deserialize<InputModel>(File.ReadAllText("input.json"));
TableInputModel tableModel = JsonSerializer.Deserialize<TableInputModel>(File.ReadAllText("Tables.json"));

Table actionTable = new Table(tableModel.ActionTable);
Table goToTable = new Table(tableModel.GotoTable);

Lr1Parser.Parse(model, actionTable,goToTable);