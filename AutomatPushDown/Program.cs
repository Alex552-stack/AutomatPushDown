using System.Text.Json;
using AutomatPushDown.Utils;
using Tema1Limbaje.Models;

InputModel model = 
    JsonSerializer.Deserialize<InputModel>(
        File.ReadAllText("input.txt")
        );
PushDown push = new(model);
push.CalculateTinis();
