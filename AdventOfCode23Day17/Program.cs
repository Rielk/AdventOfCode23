using AdventOfCode23Day17;
using AdventOfCode23Day17.Properties;

string input = Resources.Input1;

CityMap cityMap = new(input.Split(Environment.NewLine));

int minHeatLoss = cityMap.FindLowestHeatLoss();

Console.WriteLine($"Lowest possible heat loss: {minHeatLoss}");
