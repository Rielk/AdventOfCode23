using AdventOfCode23Day17;
using AdventOfCode23Day17.Properties;

string input = Resources.Input1;

CityMap cityMap = new(input.Split(Environment.NewLine));

int minHeatLoss = cityMap.FindLowestHeatLoss(1, 3);
int minUltraHeatLoss = cityMap.FindLowestHeatLoss(4, 10);

Console.WriteLine($"Lowest possible heat loss for regular crucibles: {minHeatLoss}");
Console.WriteLine();
Console.WriteLine($"Lowest possible heat loss for ultra crucibles: {minUltraHeatLoss}");
